LK_Ecommerce API
This is a complete backend API for a modern e-commerce platform, built with ASP.NET Core, Entity Framework Core, and SQL Server. The project is designed to showcase a professional, clean architecture suitable for a real-world application. It features a secure, token-based authentication system, a robust service layer for business logic, and a full suite of CRUD operations for managing products, users, orders, and more.

Technology Stack
.NET 8: The underlying platform for building the application.

ASP.NET Core: The web framework used to build the RESTful API.

Entity Framework Core 8: The Object-Relational Mapper (ORM) used for database interactions.

SQL Server: The relational database for storing all application data.

AutoMapper: A library for clean and efficient object-to-object mapping (Entities to DTOs).

JWT (JSON Web Tokens): For secure, token-based authentication and authorization.

BCrypt.Net-Next: For industry-standard password hashing and verification.

Swashbuckle (Swagger): For interactive API documentation and testing.

Features
Clean Architecture: Follows a professional Service Layer pattern, separating business logic from the API controllers.

Secure Authentication: Full user registration and login system using JWTs.

Role-Based Authorization: Endpoints are protected, with specific actions restricted to user roles (e.g., "Admin", "Buyer").

Full CRUD Operations: Complete create, read, update, and delete functionality for all major entities.

Shopping Cart Management: Full logic for adding, updating, and removing items from a user's cart.

Complete Checkout Process: Business logic to convert a shopping cart into a final order, including stock reduction and price calculation.

Pagination: Scalable endpoints for fetching large lists of data (products, orders, etc.).

Soft Deletes: Uses a "soft delete" pattern for key entities to preserve data integrity and history.

Getting Started
Follow these steps to get the project running on your local machine.

Prerequisites
.NET 8 SDK

Visual Studio 2022 or VS Code

SQL Server Express or another SQL Server instance.

A Git client.

1. Clone the Repository
git clone https://github.com/YourUsername/YourRepoName.git
cd YourRepoName

2. Configure the Database Connection
Open the appsettings.json file located in the src folder.

Find the ConnectionStrings section.

Update the DefaultConnection string to point to your local SQL Server instance. You may need to change the Server and Database names.

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LK_Ecommerce_DB;Trusted_Connection=True;TrustServerCertificate=True;"
}

3. Configure JWT Settings
In the same appsettings.json file, add a JwtSettings section.

Provide a long, random, and secret key. Do not commit a real secret key to a public repository.

"JwtSettings": {
  "SecretKey": "YourSuperLongAndSecretKeyGoesHere_DoNotUseThisOne",
  "Issuer": "YourAppName",
  "Audience": "YourAppAudience",
  "ExpiryMinutes": 60
}sni

4. Apply Database Migrations
Open a terminal in the src folder.

Run the following command to create the database and all its tables based on the project's migrations.

dotnet ef database update

5. Run the Application
From the src folder, run the application.

dotnet run

The API will now be running. You can access the Swagger UI documentation in your browser at the URL provided in the terminal (e.g., https://localhost:7094/swagger).

API Endpoints Overview
A brief overview of the main available endpoints.

Authentication

POST /api/auth/register: Create a new user account.

POST /api/auth/login: Log in and receive a JWT.

Products

GET /api/products: Get a paginated list of all products.

GET /api/products/{id}: Get a single product.

Shopping Cart

GET /api/shoppingcart: Get the current user's cart.

POST /api/shoppingcart/items: Add an item to the cart.

PATCH /api/shoppingcart/items/{itemId}: Update an item's quantity.

DELETE /api/shoppingcart/items/{itemId}: Remove an item.

Orders

POST /api/orders: Create a new order (checkout).

GET /api/orders: Get the current user's order history.
