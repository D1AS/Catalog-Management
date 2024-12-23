# App Store Game Catalog Management API

## Overview

This skeleton project serves as a foundation for managing an App Store game catalog, showcasing best practices in building a Web API using .NET 7. The application utilizes an in-memory database for simplicity and ease of development.

## Key Features

- **Separation of Concerns**: The Web API is structured into three distinct layers:
  - **API Layer**: Handles HTTP requests and responses.
  - **Application Layer**: Contains business logic and application rules.
  - **Contracts Layer**: Defines data transfer objects and interfaces.

- **CRUD Operations**: Implements Create, Read, Update operations (deletion is not supported; game packs are only deactivated).

- **Contract Mapping**: Efficient mapping between API contracts and internal data models.

- **Endpoint Management**: Organized management of API endpoints for clarity and usability.

- **Fluent Validation**: Utilizes FluentValidation for robust input validation.

- **Cancellation Tokens**: Supports cancellation tokens for request management (not propagated to the repository as it uses an in-memory database).

- **Authentication and Authorization**:
  - JWT-based authentication for secure access.
  - API key-based authentication functionality.

- **Data Handling**:
  - Filtering and pagination capabilities for efficient data retrieval.
  
- **Versioning**: Supports multiple versions of the API to maintain backward compatibility.

- **Documentation**: Integrated Swagger for interactive API documentation.

- **Health Check Endpoints**: Provides endpoints to check the health status of the application.

- **Caching Strategies**:
  - Response caching to improve performance.
  - Output caching to efficiently manage frequently accessed data.

## Getting Started

### Prerequisites

- .NET 7 SDK installed on your machine.
  
### Installation

1. Clone the repository

2. Navigate to the project directory:
cd game-catalog-management-api

3. Restore dependencies:
dotnet restore

4. Run the application:
dotnet run

## API Documentation

Once the application is running, you can access the Swagger UI at `http://localhost:<port>/swagger` to explore available endpoints and test the API functionalities interactively.

## Authentication

To access protected routes, include the JWT token in the Authorization header:

Authorization: Bearer <your_jwt_token>

For API key authentication, include your API key in the request headers:

x-api-key: <your_api_key>

## Contributing

Contributions are welcome! If you would like to contribute to this project, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
