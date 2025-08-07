# BugTracker Console Application

A simple yet powerful **Bug Tracking System** built using **.NET Core Console App**, applying **Clean Architecture (Onion Architecture)**, **SOLID principles**, and **AutoMapper** for efficient object mapping.

---

## Project Overview

This BugTracker project helps manage software development bugs/issues in an organized way. It allows users to:

- Add, view, update, and delete **bugs**
- Manage **users** and **projects**
- Associate bugs with projects and users (optionally extendable)
- Demonstrates clean code practices and layered architecture

---

## Architecture – Onion (Clean) Architecture

```
BugTracker
│
├── Core               --> DTOs, Interfaces, Entities
│
├── Application        --> Services (Business Logic Layer)
│
├── Infrastructure     --> Repositories (Data Access Layer - In-Memory)
│
└── ConsoleApp         --> Presentation Layer (Menu-Driven Console UI)
```

This approach ensures **separation of concerns**, **testability**, and **scalability**.

---

## Technologies Used

- ✅ .NET 8 Console App
- ✅ C# 12
- ✅ AutoMapper v12
- ✅ SOLID Principles
- ✅ In-Memory Repositories
- ✅ Onion Architecture

---

## Key Features

### Bug Management
- Add, view, update, delete bugs
- Each bug includes: `Id`, `Title`, `Description`, `Status`, `Due Date`

### User Management
- Add, view, update, delete users
- Each user includes: `Id`, `Name`

### Project Management
- Add, view, update, delete projects
- Each project includes: `Id`, `Title`

### AutoMapper Integration
- DTOs and Entities mapped using AutoMapper Profile

---

## Folder Structure

```
BugTracker/
│
├── Core/
│   ├── DTOs/            --> Request & Response DTOs
│   ├── Entities/        --> Bug, User, Project classes
│   └── Interfaces/      --> Repositories & Service Interfaces
│
├── Application/
│   ├── Mapping/         --> AutoMapper Profile
│   └── Services/        --> Implementations of Service Interfaces
│
├── Infrastructure/
│   └── Repositories/    --> In-Memory Repo Implementations
│
└── ConsoleApp/
    └── Program.cs       --> User Interaction via Menu
```

---

## 🚀 How to Run


1. **Open in Visual Studio or VS Code**

2. **Restore NuGet Packages** (AutoMapper)

3. **Build and Run**  
  
   ```bash
   dotnet build

   cd BugTracker.ConsoleApp

   dotnet run
   ```

---

## Sample Test Cases

- Add Bug → Provide title, description, status, and due date.
- View All Bugs → Lists all bugs with due dates.
- Add User/Project → Provide minimal details.
- Update → Select by ID and update relevant fields.
- Delete → Deletes bug/user/project by ID.

---

## Validation Included

- Empty input checks for required fields
- ID validation before update/delete
- Shows "None" when no records exist

---

## Sample Output
<img width="320" height="345" alt="Screenshot 2025-08-07 143307" src="https://github.com/user-attachments/assets/1dac5ee6-4fec-4338-a22c-35373f43d30f" />
<img width="407" height="70" alt="Screenshot 2025-08-07 143416" src="https://github.com/user-attachments/assets/0d4a273a-d364-44d2-b1a7-73b27d564e8f" />
<img width="406" height="69" alt="Screenshot 2025-08-07 143422" src="https://github.com/user-attachments/assets/426b2482-5717-4a4e-9e3f-bff404433d1f" />
<img width="253" height="116" alt="Screenshot 2025-08-07 143428" src="https://github.com/user-attachments/assets/a4bd040a-cbf4-4378-b955-efaae4c73318" />
<img width="224" height="74" alt="Screenshot 2025-08-07 143437" src="https://github.com/user-attachments/assets/ae600c33-94ca-4459-a548-f0b4c5137554" />

---


## Author

**Swetha**  
.NET Training Day13
