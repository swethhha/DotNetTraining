# Day 3 - Leave Management System

A C# console-based Leave Management System built using **Object-Oriented Programming** and **SOLID design principles**. This project was developed as part of Day 3 of .NET training.

---
## ✅ Features

- Apply for different types of leaves: `CasualLeave`, `SickLeave`
- Approve or reject requests using `IApprovable` interface
- View all leave requests
- Display only approved requests
- Clean and modular design using **SOLID principles**

---

## 💡 SOLID Principles Applied

| Principle | How It’s Used |
|----------|----------------|
| **S - Single Responsibility** | Each class (like `LeaveRequest`, `LeaveService`) has a focused responsibility |
| **O - Open/Closed**           | New leave types (like `SickLeave`) can be added without modifying existing code |
| **L - Liskov Substitution**   | `CasualLeave` and `SickLeave` can be used wherever `LeaveRequest` is expected |
| **I - Interface Segregation** | Interface `IApprovable` ensures separation of approval responsibility |
| **D - Dependency Inversion** | `ILeaveService` allows for loosely coupled service logic |

---

## 🛠 Technologies Used

- **C#**
- **.NET Console Application**
- **Visual Studio 2022 / VS Code**

---

## 🧭 Steps

1. **Open terminal and navigate to project directory:**

   ```bash
   cd path\\to\\Day2proj1phase2

2. **Build the project:**

   ```bash
    dotnet build
2. **Run the application:**

   ```bash
    dotnet run

---
## Sample Output:

   <img width="620" height="164" alt="Screenshot 2025-07-24 112632" src="https://github.com/user-attachments/assets/a22cf14d-aac8-4c79-b2fd-ec81ccb2dac5" />

---

### 🧑‍💻 Author

**Swetha T**  
.NET Training – Day 3

---

### 📜 License

This project is for **educational and demonstration purposes only**.
