# Dener Makine

<p>An ASP.NET Core MVC application for managing guides, departments, and content with file upload capabilities. Developed during a voluntary internship at Dener Makine.</p>

## ✨ Features

- 📚 **Guide Management** - List guides with department information and detailed pages
- 🎥 **Video Chapters** - Organize video content into chapters
- 🔍 **Search Functionality** - Search by guide name, description, and department
- 📤 **File Management** - Upload and download guide files
- 🔐 **User Authentication** - Cookie-based login and logout
- 🛠️ **Admin Dashboard** - Manage departments, guides, users, and content
- 📊 **Entity Framework** - MySQL active setup, SQL Server-compatible backup migrations included

## 🛠️ Technology Stack

| Technology | Version |
|---|---|
| **.NET** | 9 |
| **ASP.NET Core MVC** | 9 |
| **Entity Framework Core** | 9 |
| **Database** | MySQL (active), SQL Server (optional) |
| **Authentication** | Cookie-based |

## 📋 Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server 8+](https://dev.mysql.com/downloads/mysql/)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (optional)

## ⚙️ Configuration

**Database Connection String:**

The active connection is configured from `appsettings*.json` and used in `Program.cs`.

MySQL example:

```json
"ConnectionStrings": {
   "DefaultConnection": "Server=localhost;Port=3306;Database=DenerMakine;User=root;Password=your_password;"
}
```

SQL Server option (for contributors who continue with MSSQL):

```text
Server=(localdb)\MSSQLLocalDB;Database=DenerMakine;Integrated Security=true
```

**Default Admin Account:**

```
Email:    admin@admin.com
Password: adm1234
```

## 🚀 Getting Started

### Installation

1. **Clone and restore dependencies:**
   ```bash
   git clone <repository-url>
   cd Project1
   dotnet restore
   ```

2. **Set up the database:**
   ```bash
   dotnet ef database update
   ```

3. **Run the application:**
   ```bash
   dotnet run
   ```

The application will be available at `http://localhost:5000` or the configured port in `launchSettings.json`.

## 📁 Project Structure

```
DenerMakine/
├── Controllers/          # MVC Controllers
├── Views/               # Razor Views
├── Areas/Admin/         # Admin panel features
├── Data/                # Database context
├── Entities/            # Domain models
├── Migrations/          # EF Core migrations
├── ViewComponents/      # Reusable view components
├── wwwroot/            # Static files (CSS, JS, images)
└── Program.cs          # Application entry point
```

## 🔑 Key Entities

- **Department** - Organization units for guides
- **Guide** - Content guides with descriptions
- **VideoChapter** - Video content organized by chapters
- **User** - Application users for authentication

## 📝 Database Migrations

Migrations are tracked in version control. The active MySQL migration set is under `Migrations`, and legacy SQL Server migrations are preserved under `MigrationsSqlServerBackup`.

When pulling changes, apply pending migrations:

```bash
dotnet ef database update
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project was developed as part of an internship program.
```

Default development ports:

- `http://localhost:5055`
- `https://localhost:7028`

## Main Routes

- `/` - Home page and guide list
- `/Guides` - Guide list
- `/Guides/Details/{id}` - Guide details page
- `/Guides/Search` - Search results
- `/Departments/Index/{id}` - Guides for a department
- `/Login/Login` - Login page
- `/Logout` - Logout action

## Project Structure

- `Controllers` - Public site controllers
- `Areas/Admin` - Admin area views and controllers
- `Data` - Database context class
- `Entities` - EF Core entity models
- `Views` - Public site MVC views
- `wwwroot` - Static files, images, downloads, and scripts

## Notes

- Guide files are served from `wwwroot/files/guides`.
- The app uses cookie authentication and redirects unauthenticated users to the login page.
- The admin area is intended for managing the content shown on the public site.


--Bashkal