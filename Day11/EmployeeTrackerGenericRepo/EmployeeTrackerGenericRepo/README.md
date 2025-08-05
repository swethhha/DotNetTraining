# EmployeeTracker – Generic Repository Pattern

## Overview

**EmployeeDeptTracker** is a C# Console Application that demonstrates full CRUD operations on **Departments** and **Employees** using **Generic Repository Pattern**. The application uses a simple, menu-driven console interface and includes validations to ensure clean user inputs.

---

## Technologies Used

- .NET 8.0
- Entity Framework Core
- Console-based UI

---

## Project Structure

```
EmployeeTracker/
│
├── Core/ # Core entities and interfaces
│ ├── Entities/
│     ├── Department.cs
│     └── Employee.cs
│ └── Interfaces/
│     ├── IDepartmentRepository.cs
│     └── IEmployeeRepository.cs
│
├── Infrastructure/
│ ├── Repositories/
│     ├── DepartmentRepository.cs
│     └── EmployeeRepository.cs
│
├── Application/ 
│ └── Services/
│     ├── DepartmentService.cs
│     └── EmployeeService.cs
│
└── ConsoleApp/ # Console UI
    └── Program.cs

```

---

## Features

- CRUD operations for **Departments**:

  - Add Department
  - View All Departments
  - Update Department
  - Delete Department

- CRUD operations for **Employees**:

  - Add Employee
  - View All Employees
  - Update Employee
  - Delete Employee

- **Validations**:

  - Non-empty names
  - Unique Department IDs starting from 1
  - Required field checks for Employee: Name, Email, DeptId

---

## How to Run

```bash

# 1. Build project
> dotnet build

# 2. Run application
> dotnet run
```

---

## Sample Menu UI

```bash
=== Employee Tracker System ===
1. Add Department
2. View All Departments
3. Update Department
4. Delete Department
5. Add Employee
6. View All Employees
7. Update Employee
8. Delete Employee
9. Exit
Enter your choice:
```

---

## Sample Output

```bash
--- Add New Employee ---
Enter Employee Name: Alice
Enter Department ID: 1
Employee added successfully!

--- All Employees ---
ID: 1, Name: Alice, Department: HR

--- Update Employee ---
Enter Employee ID to update: 1
Enter new Name: Alice M
Enter new Department ID: 2
Employee updated successfully!

--- Delete Employee ---
Enter Employee ID to delete: 1
Employee deleted successfully!

```

---

## Author

**Swetha T**\
.NET Training – Day 11

