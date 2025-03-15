# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base image for running the application in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Set environment variables for HTTP and HTTPS ports and Kestrel certificate configuration
ENV HTTP_PORTS=8080
ENV HTTPS_PORTS=8081
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/otel-demo-api.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=password123

# Stage for building the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy the project file and restore dependencies
COPY ["OtelDemoApi/OtelDemoApi.csproj", "OtelDemoApi/"]
COPY ["OtelDemoApi.ServiceDefaults/OtelDemoApi.ServiceDefaults.csproj", "OtelDemoApi.ServiceDefaults/"]
# Restore dependencies for the entire solution
RUN dotnet restore "OtelDemoApi/OtelDemoApi.csproj"
# Copy the remaining source code and build the project
COPY ["OtelDemoApi/", "OtelDemoApi/"]
COPY ["OtelDemoApi.ServiceDefaults/", "OtelDemoApi.ServiceDefaults/"]
WORKDIR "/src/OtelDemoApi"
RUN dotnet build "OtelDemoApi.csproj" -c Release -o /app/build

# Stage for publishing the service project
FROM build AS publish
# Publish the project to the /app/publish directory
RUN dotnet publish "OtelDemoApi.csproj" -c Release -o /app/publish

# Final stage for running the application in production or regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
# Copy the published output from the publish stage
COPY --from=publish /app/publish .
# Copy the HTTPS certificate to the container
COPY ["Https/otel-demo-api.pfx", "/https/otel-demo-api.pfx"]
# Set the entry point to run the application
ENTRYPOINT ["dotnet", "OtelDemoApi.dll"]