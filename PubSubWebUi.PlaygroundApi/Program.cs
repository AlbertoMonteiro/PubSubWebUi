using Microsoft.AspNetCore.Mvc;
using PubSubWebUi.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

var emulatorHost = new Uri("http://localhost:8681/");

builder.AddServiceDefaults();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRefitClient<IPubSubService>()
    .ConfigureHttpClient(client => client.BaseAddress = emulatorHost);

var app = builder.Build();
app.MapOpenApi();
app.MapSwagger();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/callback", async (HttpResponseMessage msg, ILogger<HttpResponseMessage> logger) =>
{
    logger.LogInformation("Received pubsub push message: {Message}", await msg.Content.ReadAsStringAsync());
    return TypedResults.Ok();
});

app.MapGet("/topics", async ([FromQuery] string projectId, IPubSubService pubSubService) =>
{
    var response = await pubSubService.GetTopicsAsync(projectId);
    return TypedResults.Ok(response.Content);
});

await app.RunAsync();