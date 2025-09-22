# Architecture Overview

This project follows a layered architecture:

- **Api/** → Controllers (HTTP endpoints)
- **Application/** → Services, DTOs, Validators (business logic)
- **Domain/** → Core entities and interfaces
- **Infrastructure/** → Data access, repositories, EF Core DbContext
- **Common/** → Middleware, helpers

### Key Practices
- **Dependency Injection**: All services and repositories are injected via constructor.
- **Separation of Concerns**: Each layer has a single responsibility.
- **DTOs**: Used for request/response instead of exposing entities directly.
- **Validators**: FluentValidation is used to validate inputs.
