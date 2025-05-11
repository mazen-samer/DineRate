# DineRate 🍽️

DineRate is a restaurant review API built using **ASP.NET Core Web API** and **Entity Framework Core**. It allows users to register, log in, browse restaurants, leave reviews, and react (like/dislike) to reviews.

---

## 🚀 Features

- ✅ JWT Authentication (Register & Login)
- ✅ Role-based Authorization (Admin/User)
- ✅ Restaurant CRUD (Admin-only)
- ✅ Review system (Users post reviews for restaurants)
- ✅ Like/Dislike reactions on reviews
- ✅ Lazy Loading with EF Core Proxies
- ✅ AutoMapper for clean DTO handling
- ✅ Search restaurants by name
- ✅ Filter restaurants by cuisine

---

## 🧱 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- JWT Authentication
- Postman

---

## 📂 Project Structure

```
DineRate/
│
├── Controllers/             # API endpoints
├── DTO/                     # Data Transfer Objects
├── Models/                  # Entity models
├── Data/                    # EF Core DbContext
├── Repositories/            # Interfaces and classes for data access
├── Services/                # JWT token generation, auth helpers
├── Program.cs               # Main entry point
├── appsettings.json         # Configs and connection strings
```

---

## 🛠️ Setup Instructions

1. **Clone the repository**

   ```bash
   git clone https://github.com/your-username/DineRate.git
   ```

2. **Navigate into the project**

   ```bash
   cd DineRate
   ```

3. **Update database connection in `appsettings.json`**

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER_HERE;Database=DATABASE_NAME_HERE;Trusted_Connection=True;"
   }
   ```

4. **Run database migrations**

   ```bash
   dotnet ef database update
   ```

5. **Run the API**

   ```bash
   dotnet run
   ```

6. **Test using Postman**

---

## 🔐 Authentication

- JWT-based login and protected endpoints
- After login, copy the token and use in headers:
  ```
  Authorization: Bearer <your_token_here>
  ```

---

## 🔁 Sample Endpoints

### 🧾 Register

```
POST /api/User/register
Body:
{
  "username": "Mazen",
  "email": "mazen@gmail.com",
  "password": "mazen3042000"
}
```

### 🔓 Login

```
POST /api/User/login
Body:
{
  "username": "Mazen",
  "password": "mazen3042000"
}
```

### 🍴 Create a Restaurant (Admin only)

```
POST /api/Restaurant
Header: Authorization: Bearer <token>
Body:
{
  "name": "Pizza Planet",
  "cuisine": "Italian",
  "location": "Cairo"
}
```

### 💬 Post a Review

```
POST /api/Review
Header: Authorization: Bearer <token>
Body:
{
  "restaurantId": 1,
  "rating": 5,
  "comment": "Amazing!"
}
```

### 👍 React to a Review

```
POST /api/ReviewReaction
Header: Authorization: Bearer <token>
Body:
{
  "reviewId": 2,
  "isLike": true
}
```

---

## 🔍 Extra Functionality

- **Search restaurants**

  ```
  GET /api/Restaurant/search?name=Pizza
  ```

- **Filter restaurants by cuisine**

  ```
  GET /api/Restaurant/filter?cuisine=Mexican
  ```

- **Get review reactions (likes/dislikes)**
  ```
  GET /api/ReviewReaction/likes/{reviewId}
  GET /api/ReviewReaction/dislikes/{reviewId}
  ```

---

## 🤝 Contributions

Feel free to open pull requests or suggest improvements!

---

## 📧 Contact

Want to collaborate or give feedback? Reach out on [LinkedIn](https://www.linkedin.com) or open an issue!

---
