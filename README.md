# PubSubWebUi 💬

PubSubWebUi is a modern web interface for managing Pub/Sub (Publisher/Subscriber) messaging systems. Built with .NET 10 and Blazor, it provides an intuitive user interface for managing topics and subscriptions in your messaging infrastructure.

## 📦 NuGet Package - .NET Aspire Integration

[![NuGet](https://img.shields.io/nuget/v/PubSubWebUi.Aspire.Hosting.svg)](https://www.nuget.org/packages/PubSubWebUi.Aspire.Hosting/) [![Downloads](https://img.shields.io/nuget/dt/PubSubWebUi.Aspire.Hosting.svg)](https://www.nuget.org/packages/PubSubWebUi.Aspire.Hosting/)

The **PubSubWebUi.Aspire.Hosting** package provides seamless integration with .NET Aspire for hosting Google Cloud Pub/Sub Emulator with an optional Web UI.

## ⭐ Features

- 📑 **Topic Management**
  - View list of topics
  - Create new topics
  - View topic details
- 📨 **Subscription Management**
  - View list of subscriptions
  - Create new subscriptions
  - View subscription details
  - Pull subscription messages
  - Delete subscriptions
- ⚡ **Modern Blazor-based UI**
- 💻 **Responsive Layout with Navigation Menu**
- 🔌 **.NET Aspire Integration**
- 🐳 **Container-based Deployment**
- 🌐 **Multiple Deployment Options**

## 🚀 Getting Started

### Prerequisites

- 🌐 A modern web browser
- 🐳 Docker (for running the emulator containers)
- 📦 .NET 8.0+ SDK (only if building from source)

## 🚀 Deployment Options

### Option 1: Docker Run (Simplest)

```bash
# Run Pub/Sub Emulator
docker run -d --name pubsub-emulator \
  -p 8681:8681 \
  messagebird/gcloud-pubsub-emulator:latest \
  gcloud beta emulators pubsub start --host-port=0.0.0.0:8681

# Run Web UI
docker run -d --name pubsub-webui \
  -p 8080:8080 \
  -e PUBSUB_EMULATOR_HOST=http://host.docker.internal:8681 \
  -e GCP_PROJECT_IDS=test-project,my-project \
  ghcr.io/albertomonteiro/pubsubwebui:latest
```

Access the Web UI at: `http://localhost:8080`

### Option 2: Docker Compose (Recommended)

Create a `docker-compose.yml` file:

```yaml
version: '3.8'

services:
  pubsub-emulator:
    image: messagebird/gcloud-pubsub-emulator:latest
    ports:
      - "8681:8681"

  pubsub-webui:
    image: ghcr.io/albertomonteiro/pubsubwebui:latest
    ports:
      - "8080:8080"
    environment:
      - PUBSUB_EMULATOR_HOST=http://pubsub-emulator:8681
      - GCP_PROJECT_IDS=test-project,my-project
      - ASPNETCORE_ENVIRONMENT=Development
    depends_on:
      - pubsub-emulator
```

Run with:
```bash
docker-compose up -d
```

### Option 3: .NET Aspire Integration (For .NET Developers)

If you're already using .NET Aspire, you can use our convenience package:

1. **Install the package:**
   ```bash
   dotnet add package PubSubWebUi.Aspire.Hosting
   ```

2. **Add to your AppHost Program.cs:**
   ```csharp
   var builder = DistributedApplication.CreateBuilder(args);

   // Add Pub/Sub Emulator with Web UI
   var pubsub = builder.AddPubSubEmulator("pubsub-emulator")
                      .WithWebUi("my-project,another-project");

   // Reference in your services
   builder.AddProject<Projects.MyService>("my-service")
          .WithReference(pubsub);

   builder.Build().Run();
   ```

#### 🔧 .NET Aspire Configuration Options

##### Basic Emulator
```csharp
builder.AddPubSubEmulator("pubsub-emulator", port: 8681)
```

##### With Custom Port
```csharp
builder.AddPubSubEmulator("pubsub-emulator")
       .WithHostPort(9090)
```

##### With Web UI
```csharp
builder.AddPubSubEmulator("pubsub-emulator")
       .WithWebUi("project1,project2,project3")
```

##### Advanced Web UI Configuration
```csharp
builder.AddPubSubEmulator("pubsub-emulator")
       .WithWebUi("my-project", container => 
           container.WithEnvironment("CUSTOM_VAR", "value")
                   .WithEndpoint(9000, targetPort: 8080, name: "custom-ui"));
```

### Option 4: Build from Source

1. **Clone and build:**
   ```bash
   git clone <repository-url>
   cd PubSubWebUi
   dotnet restore
   dotnet run --project PubSubWebUi.AppHost
   ```

## 🔧 Configuration

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `PUBSUB_EMULATOR_HOST` | Pub/Sub emulator endpoint | `http://localhost:8681` |
| `GCP_PROJECT_IDS` | Comma-separated project IDs | `test-project` |

### Using with Your Application

Once running, configure your application to use the emulator:

```bash
# Set environment variable
export PUBSUB_EMULATOR_HOST=http://localhost:8681

# Or in your application code (.NET)
Environment.SetEnvironmentVariable("PUBSUB_EMULATOR_HOST", "http://localhost:8681");
```

```python
# Python
import os
os.environ["PUBSUB_EMULATOR_HOST"] = "http://localhost:8681"
```

```javascript
// Node.js
process.env.PUBSUB_EMULATOR_HOST = "http://localhost:8681";
```

### 🌐 Accessing the Services

- **Pub/Sub Emulator**: `http://localhost:8681`
- **Web UI**: `http://localhost:8080`
- **Environment Variable**: `PUBSUB_EMULATOR_HOST` is automatically configured

## 🔨 Project Structure

The solution consists of multiple projects:

- **PubSubWebUi**: The main web application containing the Blazor UI components and core functionality
- **PubSubWebUi.ServiceDefaults**: Common service configuration and defaults
- **PubSubWebUi.AppHost**: Application host configuration and orchestration
- **PubSubWebUi.Aspire.Hosting**: .NET Aspire integration package for easy Pub/Sub emulator hosting

### ⚙️ Key Components

- **Services**
  - `IPubSubService`: Interface for Pub/Sub operations
  - `ProjectContext`: Application context management

- **Components**
  - **Blocks**: Reusable UI components for topics and subscriptions
  - **Dialogs**: Modal dialogs for creating new topics and subscriptions
  - **Layout**: Application layout components including navigation
  - **Pages**: Main application pages
  - **Models**: Data models for topics and subscriptions

- **Aspire Extensions**
  - `PubSubEmulatorExtensions`: Extension methods for .NET Aspire integration
  - `PubSubEmulatorResource`: Resource definition for the emulator

## 🐳 Docker Support

The package uses the following container images:
- **Pub/Sub Emulator**: `messagebird/gcloud-pubsub-emulator:latest`
- **Web UI**: `ghcr.io/albertomonteiro/pubsubwebui:latest`

## 👨‍💻 Development

This project uses:
- ⚡ .NET 8.0, 9.0, and 10.0 support
- 🌐 Blazor for the web UI
- 🔄 Modern C# features
- 🏗️ Component-based architecture
- 📦 .NET Aspire for orchestration (optional)
- 🐳 Container-based deployment

## 📚 API Reference

### PubSubEmulatorExtensions

#### `AddPubSubEmulator(string name, int port = 8681)`
Adds a Google Cloud Pub/Sub emulator resource to the application model.

#### `WithHostPort(int? port)`
Configures the host port that the Pub/Sub emulator resource is exposed on.

#### `WithWebUi(string projectsIds = "test-project", Func<...>? configurer = null)`
Adds a Pub/Sub Web UI container and configures it to connect to the specified Pub/Sub emulator resource.

## 📄 License

MIT

## 🤝 Contributing

We love your input! We want to make contributing to PubSubWebUi as easy and transparent as possible, whether it's:

- Reporting a bug
- Discussing the current state of the code
- Submitting a fix
- Proposing new features
- Becoming a maintainer

### Development Process

1. Fork the repo and create your branch from `main`
2. If you've added code that should be tested, add tests
3. If you've changed APIs, update the documentation
4. Ensure the test suite passes
5. Make sure your code follows the existing style
6. Issue that pull request!

### Any contributions you make will be under the MIT Software License

In short, when you submit code changes, your submissions are understood to be under the same `[MIT License](http://choosealicense.com/licenses/mit/)` that covers the project. Feel free to contact the maintainers if that's a concern.

### Report bugs using GitHub's `[issue tracker](<repository-url>/issues)`

We use GitHub issues to track public bugs. Report a bug by `[opening a new issue](<repository-url>/issues/new)`; it's that easy!

### Write bug reports with detail, background, and sample code

**Great Bug Reports** tend to have:

- A quick summary and/or background
- Steps to reproduce
  - Be specific!
  - Give sample code if you can
- What you expected would happen
- What actually happens
- Notes (possibly including why you think this might be happening, or stuff you tried that didn't work)

### License

By contributing, you agree that your contributions will be licensed under its MIT License.

## 🔗 Links

- `[NuGet Package](https://www.nuget.org/packages/PubSubWebUi.Aspire.Hosting/)`
- `[GitHub Repository](<repository-url>)`
- `[.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)`
- `[Google Cloud Pub/Sub Documentation](https://cloud.google.com/pubsub/docs)`

---

**Made with ❤️ by Alberto Monteiro**