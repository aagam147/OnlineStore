# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["OnlineStoreWorker/OnlineStoreWorker.csproj", "OnlineStoreWorker/"]
RUN dotnet restore "OnlineStoreWorker/OnlineStoreWorker.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/OnlineStoreWorker"
RUN dotnet build "OnlineStoreWorker.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "OnlineStoreWorker.csproj" -c Release -o /app/publish

# Use the official .NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS final
WORKDIR /app

# Copy the published application from the build stage
COPY --from=publish /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "OnlineStoreWorker.dll"]
