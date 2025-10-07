# PubSubWebUi 💬

PubSubWebUi is a modern web interface for managing Pub/Sub (Publisher/Subscriber) messaging systems. Built with .NET 10 and Blazor, it provides an intuitive user interface for managing topics and subscriptions in your messaging infrastructure.

## ⭐ Features

- 📑 Topic Management
  - View list of topics
  - Create new topics
  - View topic details
- 📨 Subscription Management
  - View list of subscriptions
  - Create new subscriptions
  - View subscription details
- ⚡ Modern Blazor-based UI
- 💻 Responsive Layout with Navigation Menu

## 🔨 Project Structure

The solution consists of three main projects:

- **PubSubWebUi**: The main web application containing the Blazor UI components and core functionality
- **PubSubWebUi.ServiceDefaults**: Common service configuration and defaults
- **PubSubWebUi.AppHost**: Application host configuration and orchestration

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

## 🚀 Getting Started

### Prerequisites

- 📦 .NET 10 SDK
- 🌐 A modern web browser
- 🔧 Your preferred IDE (Visual Studio 2022+ recommended)

### Running the Application

1. Clone the repository
    ```bash
    git clone <repository-url>
    ```

2. Navigate to the project directory
    ```bash
    cd PubSubWebUi
    ```

3. Restore dependencies
    ```bash
    dotnet restore
    ```

4. Run the application
    ```bash
    dotnet run
    ```

The application will be available at `https://localhost:5001` (or the configured port).

## 👨‍💻 Development

This project uses:
- ⚡ .NET 10
- 🌐 Blazor for the web UI
- 🔄 Modern C# features
- 🏗️ Component-based architecture

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

In short, when you submit code changes, your submissions are understood to be under the same [MIT License](http://choosealicense.com/licenses/mit/) that covers the project. Feel free to contact the maintainers if that's a concern.

### Report bugs using GitHub's [issue tracker](<repository-url>/issues)

We use GitHub issues to track public bugs. Report a bug by [opening a new issue](<repository-url>/issues/new); it's that easy!

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