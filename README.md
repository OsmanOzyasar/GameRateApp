
# 🎮 GameRateApp – Layered Architecture Web API with JWT Authentication

**GameRateApp** is a robust ASP.NET Core Web API project built with a real-world enterprise structure. It allows users to register, log in, and interact with a game rating system via secured endpoints using JWT authentication and role-based authorization.

---

## 📚 Features

- ASP.NET Core Web API (.NET 6+)
- Entity Framework Core
- 3-Tier Architecture (Presentation → Business → Data Access)
- JWT Authentication with token generation
- Role-based Authorization (Admin / User)
- Swagger API Documentation
- DTO + AutoMapper Usage
- Clean, scalable, and maintainable codebase

---

## 🧱 Project Architecture

```
GameRateApp/
├── GameRateApp.API           # Presentation Layer (Controllers, Swagger)
├── GameRateApp.Business      # Business Logic Layer (Services, Interfaces)
├── GameRateApp.DataAccess    # Data Access Layer (Repositories)

```

---

## 🚀 Getting Started

> Note: SQL Server must be installed. Connection string should be updated in `appsettings.json`.

### 1. Clone the repository

```bash
git clone https://github.com/OsmanOzyasar/GameRateApp.git
cd GameRateApp
```

### 2. Restore NuGet packages

```bash
dotnet restore
```

### 3. Apply migrations and create the database

```bash
cd GameRateApp.API
dotnet ef database update
```

### 4. Run the application

```bash
dotnet run
```

### 5. Access Swagger UI

> http://localhost:5000/swagger  
(Port may vary depending on `launchSettings.json`)

---

## 🔐 Authentication and Authorization

### Auth Endpoints

| Endpoint | Description |
|----------|-------------|
| `POST /api/auth/register` | Registers a new user |
| `POST /api/auth/login` | Logs in and returns JWT token |

Use the token as follows:

```http
Authorization: Bearer <your_token>
```

### Role-Based Access

- Admins can add, update, and delete games.
- Normal users can view game data.

---

## 📑 API Endpoints Overview

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/games` | Get all games |
| GET | `/api/games/{id}` | Get game by ID |
| POST | `/api/games` | Add new game *(Admin only)* |
| PUT | `/api/games/{id}` | Update a game *(Admin only)* |
| DELETE | `/api/games/{id}` | Delete a game *(Admin only)* |

---

## 🧪 Demo

![Swagger Demo](swagger-demo.png)

---

## 💼 Freelance Usage Suggestion

This project is a strong portfolio piece for freelance work in:
- **Backend API Development**
- **JWT Authentication Systems**
- **Enterprise-Level Architecture**

---

## 👨‍💻 Developer

**Osman Ozyasar**  
.NET & Web API Developer  
📫 [osman.ozyasar27@gmail.com]  
🔗 [https://www.linkedin.com/in/osman-özyaşar-332b0b24b]

---

## 📄 License

This project is licensed under the MIT License.
