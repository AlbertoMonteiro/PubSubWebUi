using Refit;
using System.Globalization;

namespace PubSubWebUi.Services;

public interface IPubSubService
{

    [Get("/v1/projects/{projectId}/topics")]
    Task<ApiResponse<TopicResponse>> GetTopicsAsync(string projectId);

    [Put("/v1/projects/{projectId}/topics/{topicName}")]
    Task<ApiResponse<HttpResponse>> CreateTopicAsync(string projectId, string topicName);

    [Put("/v1/projects/{projectId}/subscriptions/{subscriptionName}")]
    Task<ApiResponse<HttpResponse>> CreateSubscriptionAsync(string projectId, string subscriptionName, [Body] NewSubscriptionRequest request);

    [Get("/v1/projects/{projectId}/topics/{topicName}/subscriptions")]
    Task<ApiResponse<SubscriptionResponse>> GetSubscriptionsAsync(string projectId, string topicName);

    Task PublishMessageAsync(string topicName, string message);
    Task<IEnumerable<string>> PullMessagesAsync(string subscriptionName, int maxMessages = 10);
    Task DeleteTopicAsync(string topicName);
    Task DeleteSubscriptionAsync(string subscriptionName);
}

public record TopicResponse(Topic[] Topics);
public record Topic(string Name, Dictionary<string, string> Labels);

public record NewSubscriptionRequest(string Name, string Topic, PushConfig? PushConfig = null);
public record PushConfig(string PushEndpoint, Dictionary<string, string>? Attributes);

public record SubscriptionResponse(List<string> Subscriptions);
