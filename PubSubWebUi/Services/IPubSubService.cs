using Refit;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using Attributes = System.Collections.Generic.Dictionary<string, string>;

namespace PubSubWebUi.Services;

public interface IPubSubService
{

    [Get("/v1/projects/{projectId}/topics")]
    Task<ApiResponse<TopicResponse>> GetTopicsAsync(string projectId);

    [Put("/v1/projects/{projectId}/topics/{topicName}")]
    Task<ApiResponse<HttpResponse>> CreateTopicAsync(string projectId, string topicName);

    [Put("/v1/projects/{projectId}/subscriptions/{subscriptionName}")]
    Task<ApiResponse<HttpResponse>> CreateSubscriptionAsync(string projectId, string subscriptionName, [Body] NewSubscriptionRequest request);

    [Get("/v1/projects/{projectId}/subscriptions")]
    Task<ApiResponse<SubscriptionResponse>> GetSubscriptionsAsync(string projectId);

    [Post("/v1/projects/{projectId}/topics/{topicName}:publish")]
    Task<ApiResponse<HttpResponse>> PublishMessageAsync(string projectId, string topicName, [Body] PublishMessagesRequest messages);

    [Post("/v1/projects/{projectId}/subscriptions/{subscriptionName}:pull")]
    Task<ApiResponse<PullMessagesResponse>> PullMessagesAsync(string projectId, string subscriptionName, [Body] PullMessagesRequest request);

    Task DeleteTopicAsync(string topicName);
    Task DeleteSubscriptionAsync(string subscriptionName);
}

public record TopicResponse(Topic[] Topics);
public record Topic(string Name, Attributes Labels)
{
    public string TopicName => Name.Split('/')[^1];
}

public record NewSubscriptionRequest(string Topic, PushConfig? PushConfig = null);
public record PushConfig(string PushEndpoint, Attributes? Attributes);

public record SubscriptionResponse(Subscription[] Subscriptions);

public record Subscription(string Name, string Topic, Pushconfig? PushConfig, int AckDeadlineSeconds, string MessageRetentionDuration)
{
    public string SubscriptionName => Name.Split('/')[^1];
}

public record Pushconfig();

public record PublishMessagesRequest(PubSubMessage[] Messages);

public record PubSubMessage(string Data,
                            Attributes? Attributes = null,
                            string? MessageId = null,
                            DateTimeOffset? PublishTime = null,
                            string? OrderingKey = null)
{
    [JsonIgnore]
    public string DecodedData => Encoding.UTF8.GetString(Convert.FromBase64String(Data));
}

public record PullMessagesRequest(bool ReturnImmediately = true, int MaxMessages = 10);

public record PullMessagesResponse(ReceivedMessage[] ReceivedMessages);

public record ReceivedMessage(string AckId, PubSubMessage Message);
