# E-Commerce Cart & Checkout

A fullstack e-commerce application with product browsing, shopping cart, and checkout — built with Angular and ASP.NET Core.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 20+](https://nodejs.org/)
- [SQL Server 2019+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Angular CLI (`npm install -g @angular/cli`)

## Database Setup

1. Open SQL Server Management Studio (SSMS)
2. Right-click **Databases** → **Restore Database...**
3. Select **Device**, click **...**, then **Add** and navigate to `Database/ECommerceDb.bak`
4. Click **OK** to restore
5. Alternatively, run the SQL script at `Database/init-database.sql` to create the schema and seed data manually

> If your SQL Server instance name differs from the default, update the connection string in `ECommerceApi/appsettings.json`.

## Backend

```bash
cd ECommerceApi
dotnet run
```

The API will start on `http://localhost:5284`. Swagger UI is available at `http://localhost:5284/swagger`.

## Frontend

```bash
cd ecommerce-frontend
npm install
ng serve
```

The app will start on `http://localhost:4200`.

## Running Tests

Backend:

```bash
cd ECommerceApi.Tests
dotnet test
```

Frontend:

```bash
cd ecommerce-frontend
ng test
```

## Project Structure

```
├── ECommerceApi/              # ASP.NET Core Web API
│   ├── Controllers/           # API endpoints
│   ├── Services/              # Business logic
│   ├── Repositories/          # Data access (ADO.NET)
│   ├── Models/                # Entities and DTOs
│   └── Middleware/             # Exception handling
├── ECommerceApi.Tests/        # xUnit backend tests
├── ecommerce-frontend/        # Angular SPA
│   └── src/app/
│       ├── components/        # UI components
│       ├── services/          # HTTP + state management
│       ├── guards/            # Route protection
│       └── interceptors/      # JWT auth interceptor
├── Database/                  # SQL scripts and .bak file
└── README.md
```