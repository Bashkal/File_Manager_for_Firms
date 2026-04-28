# Dener Makine

This project is an ASP.NET Core MVC application developed during a voluntary internship at Dener Makine. The application is used for file upload, content viewing, and managing guides by department.

## Features

- Guide listing with department information
- Guide detail page with video chapters
- Search by guide name, description, and department
- File upload and download support
- Cookie-based login and logout
- Admin panel for managing departments, guides, and users
- Entity Framework Core with SQL Server LocalDB

## Technology

- .NET 9
- ASP.NET Core MVC
- Entity Framework Core 9
- SQL Server LocalDB
- Cookie authentication

## Requirements

- .NET 9 SDK
- SQL Server LocalDB

## Configuration

The database connection is defined in [Data/DataBaseContext.cs](Data/DataBaseContext.cs) as:

```text
Server=(localdb)\MSSQLLocalDB;Database=DenerMakine;Integrated Security=true
```

The application includes the following seeded admin account:

- Email: `admin@admin.com`
- Password: `adm1234`

## Local Setup

1. Restore the packages.
2. Apply migrations if the database has not been created yet.
3. Run the application.

```bash
dotnet restore
dotnet ef database update
dotnet run
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
