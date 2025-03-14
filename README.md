# OtelDemoApi

OtelDemoApi is a .NET 8 API project that demonstrates the use of OpenTelemetry for tracing, metrics, and logging. This project includes various configurations and services to showcase how to set up and use OpenTelemetry with ASP.NET Core.

## Project Structure

### Source Code

- **Program.cs**: The main entry point of the application. Configures logging, OpenTelemetry, and sets up the ASP.NET Core middleware pipeline.
- **Controllers/TestController.cs**: Contains API endpoints for testing different log levels (Information, Warning, Error).

### Configuration Files

- **Dockerfile**: Defines the multi-stage build process for the Docker image. It includes stages for building, publishing, and running the application.
- **docker-compose.yml**: Defines the services required for the application, including the API, OpenTelemetry Collector, Jaeger, Prometheus, Grafana, and Loki.
- **otel-collector-config.yml**: Configuration file for the OpenTelemetry Collector. Defines receivers, exporters, processors, and service pipelines for traces, metrics, and logs.
- **prometheus.yml**: Configuration file for Prometheus. Defines the scrape job for collecting metrics from the OpenTelemetry Collector.

### Certificates

- **https/otel-demo-api.pfx**: A self-signed certificate used for HTTPS configuration in the application. The certificate is referenced in the Dockerfile and used by Kestrel for HTTPS endpoints.

## Getting Started

### Prerequisites

- Docker
- Docker Compose
- .NET 8 SDK

### Building and Running the Application

1. Clone the repository:
   
2. Build and run the application using Docker Compose:
   
3. The application will be available at:
   - HTTP: `http://localhost:5000`
   - HTTPS: `https://localhost:5001`

### API Endpoints

- **GET /api/test/info**: Logs an informational message and returns a simple string response.
- **GET /api/test/warning**: Logs a warning message and returns a simple string response.
- **GET /api/test/error**: Logs an error message, simulates an exception, catches it, and logs the exception details.

### Monitoring and Observability

- **Jaeger UI**: Available at `http://localhost:16686` for viewing traces.
- **Prometheus UI**: Available at `http://localhost:9090` for viewing metrics.
- **Grafana UI**: Available at `http://localhost:3000` for creating dashboards and visualizing metrics and logs.
- **Loki**: Used for log aggregation and querying.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
