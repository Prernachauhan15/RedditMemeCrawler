# Reddit Meme Crawler (.NET 7 + EF Core + SQLite)

A simple .NET 7 Web API project that fetches top memes from [r/memes](https://www.reddit.com/r/memes/) (last 24 hours), stores them in a SQLite database using Entity Framework Core, and provides APIs to generate meme reports.

---

## 🚀 Features
- Fetch top memes from Reddit (timeframe: **last 24h**, customizable limit).
- Save memes into a local SQLite database (`memes.db`).
- Generate daily meme reports (e.g., top 5, most upvoted, recent trends).
- REST API endpoints built with **ASP.NET Core Web API**.
- Entity Framework Core with migrations for schema management.

---

## 🛠️ Tech Stack
- **.NET 7**
- **ASP.NET Core Web API**
- **Entity Framework Core (SQLite)**
- **HttpClient** for Reddit API calls
- **System.Text.Json** for deserialization

---

## 📦 Setup & Installation

### 1. Clone the repository
git clone https://github.com/<your-username>/RedditMemeCrawler.git
cd RedditMemeCrawler

### 2. Restore dependencies
dotnet restore

### 3. Apply migrations & create database
dotnet ef database update
This creates memes.db (SQLite file) in your project directory.

### 4. Run the API
dotnet run
API will be available at:
👉 https://localhost:5001 (or http://localhost:5000)

## 📡 API Endpoints
### Fetch memes from Reddit and save to DB
POST /api/meme/fetch?limit=20


limit = number of memes to fetch (default: 20).

### Get daily report (example)
GET /api/report/daily-report


## 🗂️ Project Structure
RedditMemeCrawler/
│── Controllers/
│    ├── MemeController.cs
│    ├── ReportController.cs
│── Data/
│    ├── AppDbContext.cs
│── Models/
│    ├── Meme.cs
│    ├── RedditApiResponse.cs
│── Migrations/
│── Program.cs
│── appsettings.json
│── memes.db (auto-created)


git clone https://github.com/<your-username>/RedditMemeCrawler.git
cd RedditMemeCrawler
