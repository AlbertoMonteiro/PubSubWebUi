using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var pubsubEmulator = builder.AddContainer("pubsub-emulator", "messagebird/gcloud-pubsub-emulator", "latest")
    .WithHttpEndpoint(8681, 8681, "pubsub-api");

builder.AddProject<Projects.PubSubWebUi>("pubsubwebui")
    .WaitFor(pubsubEmulator)
    .WithReference(pubsubEmulator.GetEndpoint("pubsub-api"));

builder.Build().Run();
