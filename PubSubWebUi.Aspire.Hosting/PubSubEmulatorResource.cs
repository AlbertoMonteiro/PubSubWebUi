using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public class PubSubEmulatorResource(string name, string? entrypoint = null)
    : ContainerResource(name, entrypoint)
{
    public const string PubSubEndpointName = "pubsub-api";

    public EndpointReferenceExpression Host => this.GetEndpoint(PubSubEndpointName).Property(EndpointProperty.HostAndPort);

    public EndpointReference Endpoint => this.GetEndpoint(PubSubEndpointName);
}