FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["PubSubWebUi/PubSubWebUi.csproj", "PubSubWebUi/"]
COPY ["PubSubWebUi.ServiceDefaults/PubSubWebUi.ServiceDefaults.csproj", "PubSubWebUi.ServiceDefaults/"]
RUN dotnet restore "PubSubWebUi/PubSubWebUi.csproj"
RUN dotnet restore "PubSubWebUi.ServiceDefaults/PubSubWebUi.ServiceDefaults.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "PubSubWebUi/PubSubWebUi.csproj" -c Release -o /app/build

# Publish
RUN dotnet publish "PubSubWebUi/PubSubWebUi.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PubSubWebUi.dll"]