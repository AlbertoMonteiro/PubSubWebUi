var builder = DistributedApplication.CreateBuilder(args);

var pubsubEmulator = builder.AddPubSubEmulator("pubsub-emulator")
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddContainer($"pubsub-ui", "ghcr.io/neoscript/pubsub-emulator-ui", "latest")
                .WithEndpoint(4200, targetPort: 80, name: "ui", scheme: "http")
                .WithLifetime(ContainerLifetime.Persistent)
                .WithEnvironment("GCP_PROJECT_IDS", "test-project")
                .WithEnvironment("PUBSUB_EMULATOR_HOST", pubsubEmulator.Resource.GetEndpoint("pubsub-api").Property(EndpointProperty.HostAndPort))
                .WaitFor(pubsubEmulator);

builder.AddProject<Projects.PubSubWebUi>("pubsubwebui")
    .WaitFor(pubsubEmulator)
    .WithEnvironment(pubsubEmulator);

builder.AddProject<Projects.PubSubWebUi_PlaygroundApi>("playground");

builder.Build().Run();
