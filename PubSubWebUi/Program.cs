using Microsoft.FluentUI.AspNetCore.Components;
using PubSubWebUi.Components;
using PubSubWebUi.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

var emulatorHost = builder.Configuration["PUBSUB_EMULATOR_HOST"] is { } url
    ? new Uri(url)
    : new Uri("http://localhost:8681/");

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddFluentUIComponents();

builder.Services.AddSingleton<ProjectContext>();

builder.Services.AddRefitClient<IPubSubService>()
    .ConfigureHttpClient(client => client.BaseAddress = emulatorHost);

var app = builder.Build();

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
