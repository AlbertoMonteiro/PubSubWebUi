namespace PubSubWebUi.Components.Models;

public class NewSubscription
{
    public required string Name { get; set; }
    public string? PushEndpoint { get; set; }
}