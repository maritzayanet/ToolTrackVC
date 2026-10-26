# ToolTrack — Tool Inventory Management API

A RESTful Web API built with ASP.NET Core and .NET 10 to manage tool inventory through secure, structured, and maintainable backend services.

ToolTrack was developed as a hands-on project to apply modern backend development practices, including layered architecture, data validation, authentication, database integration, containerization, and continuous integration.

## Features

- **CRUD Operations:** Create, retrieve, update, and delete tools.
- **JWT Authentication:** Token-based authentication to protect API endpoints.
- **Data Validation:** Request validation using Data Annotations and DTOs.
- **Layered Architecture:** Separation of responsibilities through Controllers, Services, and Repositories.
- **Database Integration:** SQL Server with Entity Framework Core 10.
- **Dependency Injection:** Built-in ASP.NET Core dependency injection.
- **Global Exception Handling:** Centralized handling of application errors.
- **Structured Logging:** Application logging using `ILogger`.
- **API Documentation:** Interactive Swagger / OpenAPI documentation.
- **Secure Configuration:** Sensitive local settings managed outside source control.
- **Docker Support:** Containerized application with Docker and Docker Compose.
- **Continuous Integration:** GitHub Actions workflow for automated builds and Docker image publishing.
- **Container Registry:** Public Docker image available through GitHub Container Registry (GHCR).

## Technologies

- C# and .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- Microsoft SQL Server
- JWT Bearer Authentication
- Swagger / OpenAPI
- Docker and Docker Compose
- GitHub Actions
- Git and GitHub

## Architecture

The application follows a layered architecture to separate responsibilities and improve maintainability.

`Controllers → Services → Repositories → AppDbContext → SQL Server`

- **Controllers:** Handle HTTP requests and responses.
- **Services:** Contain application logic.
- **Repositories:** Manage data access operations.
- **AppDbContext:** Provides database access through Entity Framework Core.
- **SQL Server:** Stores application data.

## Run with Docker

### Prerequisites

- Docker Desktop
- A SQL Server instance accessible to the Docker container
- Environment variables configured for the database connection and JWT authentication

### 1. Configure environment variables

Create a local `.env` file in the project directory and configure the variables expected by `compose.yaml`:

- `DOCKER_DB_CONNECTION`
- `JWT_ISSUER`
- `JWT_AUDIENCE`
- `JWT_KEY`
- `JWT_USERNAME`
- `JWT_PASSWORD`

Use your own values. Do not commit `.env` or publish passwords, connection strings, or JWT signing keys.

### 2. Start the application

```bash
docker compose pull
docker compose up -d
```

### 3. Open the API documentation

Once the container is running, open:

http://localhost:8080/swagger

You can use Swagger to explore the endpoints, authenticate, and test the API.

### 4. Stop the application

```bash
docker compose down
```

## Continuous Integration and Delivery

GitHub Actions automates the build process and publishes the Docker image to GitHub Container Registry when changes are pushed to the `main` branch.

The image can be pulled with:

```bash
docker pull ghcr.io/maritzayanet/tooltrack:latest
```

## Project Goals

This project demonstrates practical experience with:

- RESTful API development
- Backend application architecture
- Database access with Entity Framework Core
- JWT-based endpoint protection
- Containerization and reproducible environments
- Automated builds and container image publishing

## Repository

Source code: https://github.com/maritzayanet/ToolTrackVC
