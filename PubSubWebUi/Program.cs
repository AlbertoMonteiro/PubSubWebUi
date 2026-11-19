using Microsoft.FluentUI.AspNetCore.Components;
using PubSubWebUi.Components;
using PubSubWebUi.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

var emulatorHost = builder.Configuration["PUBSUB_EMULATOR_HOST"] is { } url
    ? Uri.TryCreate(url, UriKind.Absolute, out var parsed) && parsed.Scheme is "http" or "https"
        ? parsed
        : new Uri($"http://{url}/")
    : new Uri("http://localhost:8681/");

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddFluentUIComponents(config =>
{
    // Fluent UI v5 no longer applies a default 10px gap to FluentStack; keep the v4 look.
    config.DefaultValues.For<FluentStack>().Set(p => p.HorizontalGap, "10px");
    config.DefaultValues.For<FluentStack>().Set(p => p.VerticalGap, "10px");
});

builder.Services.AddSingleton<ProjectContext>();

builder.Services.AddRefitGeneratedClient<IPubSubService>(PubSubJsonContext.Default)
    .ConfigureHttpClient(client => client.BaseAddress = emulatorHost);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Using PubSub emulator in following address {EmulatorHost}", emulatorHost);

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
return 0;
