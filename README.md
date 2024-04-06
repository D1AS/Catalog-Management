# Catalog Management


A skeleton project for App Store game Catalog Management showcasing best practices building a Web Api using .NET 7.<br />
(An in memory db is used for simplicity reasons).

Points:

    • Separation of concerns by spiting the web api to three layers, api, application (business logic) and contracts
    • Crud operations (minus deletion – a game pack Never gets deleted, only deactivated)
    • Contract Mapping
    • Managing endpoints
    • Fluent Validation
    • Cancellation token (not propagated to repository as it is in memory db)
    • Authentication and Authorization using jwt
    • Api-key based auth functionality
    • Filtering and Pagination
    • Versioning
    • Swagger
    • Health check
    • Response caching
    • Output caching
