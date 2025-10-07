using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public static class PubSubEmulatorExtensions
{
    const string PUBSUB_VAR = "PUBSUB_EMULATOR_HOST";

    /// <summary>
    /// Adds a Google Cloud Pub/Sub emulator resource to the application model.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/>.</param>
    /// <param name="name">The name of the resource. This name will be used as the connection string name when referenced in a dependency.</param>
    /// <param name="port">The host port to bind the underlying container to.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<PubSubEmulatorResource> AddPubSubEmulator(this IDistributedApplicationBuilder builder, [ResourceName] string name, int port = 8681)
    {
        var resource = new PubSubEmulatorResource(name);

        return builder.AddResource(resource)
                     .WithImage(PubSubEmulatorContainerImageTags.Image)
                     .WithImageTag(PubSubEmulatorContainerImageTags.Tag)
                     .WithHttpEndpoint(port: port, targetPort: 8681, name: PubSubEmulatorResource.PubSubEndpointName);
    }

    /// <summary>
    /// Configures the host port that the Pub/Sub emulator resource is exposed on instead of using randomly assigned port.
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="port">The port to bind on the host. If <see langword="null"/> is used random port will be assigned.</param>
    /// <returns>The resource builder.</returns>
    public static IResourceBuilder<PubSubEmulatorResource> WithHostPort(this IResourceBuilder<PubSubEmulatorResource> builder, int? port)
    {
        return builder.WithEndpoint(PubSubEmulatorResource.PubSubEndpointName, endpoint => endpoint.Port = port);
    }

    /// <summary>
    /// Adds a Pub/Sub Web UI container to the application model and configures it to connect to the specified Pub/Sub emulator resource.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/>.</param>
    /// <param name="projectsIds">The GCP project IDs to use, separated by commas.</param>
    /// <param name="configurer">An optional action to further configure the container resource.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<PubSubEmulatorResource> WithWebUi(this IResourceBuilder<PubSubEmulatorResource> builder,
                                                                     string projectsIds = "test-project",
                                                                     Func<IResourceBuilder<ContainerResource>, IResourceBuilder<ContainerResource>>? configurer = null)
    {
        var container = builder.ApplicationBuilder.AddContainer("pubsub-web-ui", PubSubEmulatorContainerImageTags.UiImage, PubSubEmulatorContainerImageTags.UiTag)
                .WithEndpoint(8080, targetPort: 8080, name: "pubsub-web-ui", scheme: "http")
                .WithLifetime(ContainerLifetime.Persistent)
                .WithEnvironment("GCP_PROJECT_IDS", projectsIds)
                .WithEnvironment(PUBSUB_VAR, builder.Resource.Host);

        if (configurer is not null)
        {
            container = configurer(container);
        }
        
        container.WaitFor(builder);

        return builder;
    }
}