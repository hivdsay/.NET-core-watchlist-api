# Movie Watchlist API

A RESTful backend service for managing movies, user accounts, watchlists, reviews, and roles. Built with **.NET**, **Entity Framework Core**, and **PostgreSQL**.

---

## 🚀 Features

* User registration & authentication (JWT)
* Role-based authorization (Admin / User)
* Add, remove, update movies
* User watchlist management
* User reviews
* PostgreSQL database with EF Core migrations
* Swagger API documentation

---

## 🛠 Tech Stack

* **Backend:** .NET 8 / ASP.NET Core Web API
* **Database:** PostgreSQL
* **ORM:** Entity Framework Core
* **Auth:** JWT Bearer Authentication
* **Tooling:** Rider / Visual Studio / Docker (optional)

---

## 📦 Getting Started

### 1. Clone the repository

```bash
git clone <repo-url>
cd <project-folder>
```

### 2. Update the database connection string

Edit `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=watchlist;Username=postgres;Password=yourpassword"
}
```

### 3. Apply EF Core migrations

```bash
dotnet ef database update
```

### 4. Run the project

```bash
dotnet run
```

The API will start at:

```
http://localhost:5285
```

---

## 📘 API Documentation

Swagger UI automatically available at:

```
http://localhost:5285/swagger
```

---

## 🧩 Database Schema (Summary)

* **Users** — user accounts
* **Roles** — stored roles (Admin, User)
* **UserRole** — many-to-many link table
* **Movies** — movie list
* **Reviews** — user reviews
* **UserWatchLists** — user saved movies

---

## 🛡 Authentication

Use the `/api/Auth/login` endpoint to obtain a JWT token.

Then include the token in requests:

```
Authorization: Bearer <your-token>
```

---

## 🤝 Contributing

Pull requests are welcome! For major changes, open an issue to discuss what you’d like to change.

---

## 📄 License

This project is licensed under the MIT License.

---

If you need badges, screenshots, or detailed endpoint examples, let me know!
