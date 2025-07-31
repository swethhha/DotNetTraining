# EFCoreDbFirstDemoProject2 – StudentCourseTracker

## Abstract

This is a **C# Console Application** using **Entity Framework Core (Database-First Approach)** for managing a simple student-course enrollment system. It connects to a SQL Server database and performs full **CRUD operations** on `Students` and `Courses` using **EF Core scaffolding**.

---

## Database Setup

Execute the following SQL script in **SQL Server Management Studio (SSMS)** or **Azure Data Studio** to create the database and tables:

```sql
CREATE DATABASE StudentCourseDB;
GO

USE StudentCourseDB;
GO

CREATE TABLE Courses (
    CourseId INT PRIMARY KEY IDENTITY,
    CourseName NVARCHAR(100) NOT NULL
);

CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY,
    StudentName NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    CourseId INT FOREIGN KEY REFERENCES Courses(CourseId)
);
```

---

## Scaffold Models from Database

Use the following command to scaffold the database tables into C# model classes using EF Core:

```bash
dotnet ef dbcontext scaffold "Server=YOUR_SERVER_NAME;Database=StudentCourseDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
```

> Replace `YOUR_SERVER_NAME` with your actual SQL Server name (e.g., `localhost\SQLEXPRESS`).

---

## Project Functionalities

-  Insert a new **Student** or **Course**
-  View all **Students** and **Courses**
-  Update **Student** and **Course** details
-  Delete a **Student** or **Course**
-  **Navigation Properties** used to link Students to their Courses

---

##  Steps to Run the Application

1. **Open Terminal / Command Prompt**  
   Navigate to your project directory:

   ```bash
   cd EFCoreDbFirstDemoPhase2
   ```

2. **Build the Project**:

   ```bash
   dotnet build
   ```

3. **Run the Application**:

   ```bash
   dotnet run
   ```

---

##  Author

**Swetha T**  
.NET Training – Day 7
