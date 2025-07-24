# Day 3 - Issue Tracker Phase 3

### 📝 Abstract:
This is a **C# Console-based Issue Tracking System** designed with **SOLID principles**, emphasizing clean architecture using **abstract classes**, **interfaces**, **polymorphism**, and a **service layer**. It manages different issue types like Bugs and Features with structured reporting.

---

### 📂 Folder Structure:

| Folder     | Description |
|------------|-------------|
| Models     | Contains `Issue` (abstract base class), `Bug`, `Feature`, and `IReportable` interface. |
| Services   | Contains `IIssueService` interface and `IssueService` implementation class. |
| Program.cs | Main entry point: creates objects, calls services, and prints output. |

---

### 🧑‍💻 Components:

#### 🔹 Models

- **Issue** (abstract class)  
  Base class with properties: `Id`, `Title`, `Description`.

- **Bug** (inherits Issue)  
  Adds `Severity` and implements `IReportable`.

- **Feature** (inherits Issue)  
  Adds `IsPlanned` and implements `IReportable`.

- **IReportable** (interface)  
  Declares `string GenerateReport()` used for polymorphic reporting.

#### 🔹 Services

- **IIssueService** (interface)  
  Declares methods:
  - `void DisplayALL(List<Issue> issues)`
  - `void ShowReports(List<IReportable> issues)`

- **IssueService** (class)  
  Implements `IIssueService`, handles logic to display all issues and generate reports.

---

### ✅ Features:

- Abstract class for shared structure.
- Interface-based reporting (`IReportable`).
- Service interface and implementation (`IIssueService`, `IssueService`).
- Polymorphism using interface method `GenerateReport()`.
- Clean and modular design using **SOLID principles**.

---
## 🛠 Technologies Used

- **C#**
- **.NET Console Application**
- **Visual Studio 2022 / VS Code**

---

## 🧭 Steps

1. **Open terminal and navigate to project directory:**

   ```bash
   cd path\\to\\IssueTrackerPhase3

2. **Build the project:**

   ```bash
    dotnet build
2. **Run the application:**

   ```bash
    dotnet run

---
## Sample Output:

  <img width="907" height="271" alt="Screenshot 2025-07-24 150041" src="https://github.com/user-attachments/assets/2cbcef0a-4fcb-42c1-badd-e5e2159cbf76" />


---

### 🧑‍💻 Author

**Swetha T**  
.NET Training – Day 3

---

### 📜 License

This project is for **educational and demonstration purposes only**.

