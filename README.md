# README

> Project: User Management 

This document provides clear and professional instructions for running the project locally, along with API documentation using Swagger / OpenAPI.

---

## Project Overview

A User Management web application with a C# RESTful backend and an Angular frontend. The backend provides CRUD functionality for managing users and exposes secured API endpoints. Swagger is used for documenting and exploring the API.

### Features

* CRUD operations for **User** (Id, Name, Email, Password (hashed), DateTime)
* JWT-based authentication for secured endpoints
* Swagger / OpenAPI documentation for all routes

---

## Running the Project Locally

### Prerequisites

* .NET 7 (or latest LTS version supported by the project)
* Node.js 18+ and npm (or yarn)
* Angular CLI (if you want to develop the frontend further)

### Backend Setup

1. Open a terminal inside the `backend/` folder.
2. Restore and build dependencies:

   ```bash
   dotnet restore
   dotnet build
   ```
3. If Entity Framework Core is used, update the database:

   ```bash
   dotnet ef database update
   ```
4. Run the backend:

   ```bash
   dotnet run --project ./YourBackendProject.csproj
   ```
5. By default, the API will run at http://localhost:5238/swagger/index.html (depending on configuration).

### Frontend Setup

1. Open a terminal inside the `frontend/` folder.
2. Install dependencies:

   ```bash
   npm install
   ```
3. Start the Angular app:

   ```bash
   ng serve --open
   ```
4. The frontend will run at **[http://localhost:4200](http://localhost:4200)**.

---

## API Documentation with Swagger

### Objective

The API is documented using **Swagger** to provide an interactive UI and a machine-readable OpenAPI specification. This makes it easy to test endpoints and integrate with external tools.

### Why Swagger?

* **Interactive UI**: Test endpoints directly from the browser.
* **Standard format**: Generates `swagger.json` (OpenAPI spec) for import into Postman and other tools.
* **Auto-updating**: Automatically reflects changes in controllers and models.

### Step 1: Install Swagger

Install the Swagger NuGet package:

```bash
dotnet add package Swashbuckle.AspNetCore
```

### Step 2: Configure Swagger in Program.cs

Add the following code in `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

### Step 3: Run and Verify

1. Start the backend:

   ```bash
   dotnet run
   ```
2. Open browser and go to:

   * **Swagger UI**: `http://localhost:5110/swagger`
   * **OpenAPI JSON**: `http://localhost:5110/swagger/v1/swagger.json`

### Step 4: Add JWT Support (Optional)

If JWT authentication is enabled, add security config:

```csharp
using Microsoft.OpenApi.Models;

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token with 'Bearer ' prefix",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
```

Now, Swagger UI will show an **Authorize** button to input JWT tokens.

### Deliverables

* Swagger UI: `http://localhost:5110/swagger`
* OpenAPI spec: `swagger/v1/swagger.json`
* Optional `openapi.yaml` file included in the repository

---

## Endpoints Summary

* **POST /api/auth/login** → Authenticate and receive a JWT token.
* **POST /api/auth/register** → (Optional) Register a new user.
* **GET /api/users** → Retrieve all users (authentication required).
* **POST /api/users** → Create a new user (authentication required).
* **GET /api/users/{id}** → Retrieve a user by ID (authentication required).
* **PUT /api/users/{id}** → Update a user (authentication required).
* **DELETE /api/users/{id}** → Delete a user (authentication required).

---

## Security Notes

* Passwords must **never** be stored in plain text — use secure hashing.
* JWT tokens should be validated on each secured endpoint.
* Use HTTPS in production environments.

---

## OpenAPI (Swagger) Specification

The `openapi.yaml` file documents all available endpoints and data models. It can be imported into tools like Swagger Editor or Postman for testing.

```yaml
openapi: 3.0.3
info:
  title: User Management API
  version: 1.0.0
  description: REST API for User Management (C# backend) — CRUD secured by JWT.
servers:
  - url: http://localhost:5110
security:
  - bearerAuth: []
components:
  securitySchemes:
    bearerAuth:
      type: http
      scheme: bearer
      bearerFormat: JWT
  schemas:
    User:
      type: object
      properties:
        id:
          type: integer
          example: 1
        name:
          type: string
          example: "Divya Pawar"
        email:
          type: string
          example: "divya@example.com"
        dateTime:
          type: string
          format: date-time
          example: "2025-09-22T06:00:00Z"
      required:
        - id
        - name
        - email
        - dateTime
    UserCreate:
      type: object
      properties:
        name:
          type: string
        email:
          type: string
        password:
          type: string
        dateTime:
          type: string
          format: date-time
      required:
        - name
        - email
        - password
        - dateTime
    UserUpdate:
      type: object
      properties:
        name:
          type: string
        email:
          type: string
        password:
          type: string
    AuthRequest:
      type: object
      properties:
        email:
          type: string
        password:
          type: string
      required:
        - email
        - password
    AuthResponse:
      type: object
      properties:
        token:
          type: string
        expiresIn:
          type: integer
          example: 3600

paths:
  /api/auth/login:
    post:
      summary: Authenticate user and receive JWT
      tags: [Auth]
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/AuthRequest'
      responses:
        '200':
          description: Authentication successful
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/AuthResponse'
        '400':
          description: Invalid credentials
  /api/auth/register:
    post:
      summary: (Optional) Register a new user
      tags: [Auth]
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/UserCreate'
      responses:
        '201':
          description: User created
        '400':
          description: Validation error

  /api/users:
    get:
      summary: Get all users (requires auth)
      security:
        - bearerAuth: []
      tags: [Users]
      responses:
        '200':
          description: List of users
          content:
            application/json:
              schema:
                type: array
                items:
                  $ref: '#/components/schemas/User'
    post:
      summary: Create a new user (requires auth)
      security:
        - bearerAuth: []
      tags: [Users]
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/UserCreate'
      responses:
        '201':
          description: User created

  /api/users/{id}:
    parameters:
      - name: id
        in: path
        required: true
        schema:
          type: integer
    get:
      summary: Get user by id (requires auth)
      security:
        - bearerAuth: []
      tags: [Users]
      responses:
        '200':
          description: User object
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/User'
        '404':
          description: Not found
    put:
      summary: Update user by id (requires auth)
      security:
        - bearerAuth: []
      tags: [Users]
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/UserUpdate'
      responses:
        '200':
          description: User updated
    delete:
      summary: Delete user by id (requires auth)
      security:
        - bearerAuth: []
      tags: [Users]
      responses:
        '204':
          description: User deleted
```
