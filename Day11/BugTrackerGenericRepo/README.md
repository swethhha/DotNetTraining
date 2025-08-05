# 🐞 BugTrackerGenericRepo – Generic Repository Pattern

## Overview

**BugTrackerGenericRepo** is a C# Console Application that demonstrates the use of the **Generic Repository Pattern** using **Entity Framework Core**. It is designed for managing bug tracking operations, including projects, users and bugs, while keeping the architecture clean and maintainable.

---

## Technologies Used

- .NET 8.0
- Entity Framework Core
- Generic Repository Pattern
- Console-based UI
  
---

## Project Structure

```
BugTrackerGenericRepo/
│
├── Core/
│   └── Entities/
│       ├── Project.cs
│       ├── User.cs
│       └── Bug.cs
│   └── Interfaces/
│       ├── IProjectRepository.cs
│       └── IRepository.cs
│       └── IUserRepository.cs
│       └── IBugRepository.cs
│
├── Infrastructure/
│   └── Repositories/
│       └── ProjectRepository.cd
│       └── Repository.cs
│       └── UserRepository.cs
│       └── BugRepository.cs
│
├── Application/
│   └── Services/
│       └── UserService.cs
│       └── BugService.cs
│       └── ProjectService.cs
│
├── ConsoleUI/
│   └── Program.cs
│
├── BugTrackerGenericRepo.sln
└── README.md
```

---

## Features

- Create, Read, Update, Delete (CRUD) operations for **Users** and **Bugs** and **Projects**
- Console-based menu for interacting with the system
- Validations for input (e.g., name not empty, valid status/priority)
- Automatically sets `CreatedAt` for bugs
- Default values for bug status and priority

---

## Instructions to Run the Project: 


```bash
dotnet build
dotnet run --project ConsoleUI
```

---

## Sample Operations

### User Operations

- Add User
- View All Users
- Update User
- Delete User

### Bug Operations

- Add Bug (all fields: title, description, status, priority)
- View All Bugs
- Update Bug
- Delete Bug

### Project Operations

- Add Project
- View All Projects
- Update Project
- Delete Project
---
## Sample Output

```bash
--- Add New Bug ---
Enter Bug Title: Login Issue
Enter Bug Description: Error occurs on submitting login form
Enter Bug Priority (Low/Medium/High): High
Bug added successfully!

--- All Bugs ---
ID: 1
Title: Login Issue
Description: Error occurs on submitting login form
Status: Open
Priority: High
Created At: 05-08-2025 10:45:22 AM
----------------------------

--- Update Bug ---
Enter Bug ID to update: 1
Enter New Title: Login Form Crash
Enter New Description: App crashes while submitting credentials
Enter New Status (Open/In Progress/Resolved): In Progress
Enter New Priority (Low/Medium/High): Medium
Bug updated successfully!

--- Delete Bug ---
Enter Bug ID to delete: 1
Bug deleted successfully!

```
---
## Author

**Swetha T**  
.NET Training – Day 11
