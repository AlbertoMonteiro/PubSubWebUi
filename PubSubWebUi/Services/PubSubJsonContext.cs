using System.Text.Json;
using System.Text.Json.Serialization;

namespace PubSubWebUi.Services;

/// <summary>
/// Source-generated JSON metadata for the Pub/Sub emulator REST API models used by <see cref="IPubSubService"/>.
/// Refit 17 generated clients do not fall back to reflection-based JSON, so every request/response body type must be listed here.
/// </summary>
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(TopicResponse))]
[JsonSerializable(typeof(SubscriptionResponse))]
[JsonSerializable(typeof(NewSubscriptionRequest))]
[JsonSerializable(typeof(PublishMessagesRequest))]
[JsonSerializable(typeof(PullMessagesRequest))]
[JsonSerializable(typeof(PullMessagesResponse))]
[JsonSerializable(typeof(AcksMessagesRequest))]
// "Pushconfig" and "PushConfig" only differ by case, which collides in the generated file names.
[JsonSerializable(typeof(Pushconfig), TypeInfoPropertyName = "SubscriptionPushConfig")]
public sealed partial class PubSubJsonContext : JsonSerializerContext;
