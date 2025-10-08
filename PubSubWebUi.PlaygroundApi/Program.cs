var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/callback", async (HttpResponseMessage msg, ILogger<HttpResponseMessage> logger) =>
{
    logger.LogInformation("Received pubsub push message: {Message}", await msg.Content.ReadAsStringAsync());
    return TypedResults.Ok();
});

app.Run();