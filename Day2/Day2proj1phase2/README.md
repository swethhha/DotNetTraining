# Day2Proj1Phase2 – IssueTracker Enhancement

A console-based **Issue Tracking System** built using **C# and .NET**, demonstrating OOP enhancements including inheritance, interfaces, polymorphism, status handling, and status counting.

---

## Project Highlights

### Issue Base Class
- Fields: `Id`, `Title`, `AssignedTo`, `Status` (string-based: "Open", "In Progress", "Closed")
- Methods:
  - `AdvanceStatus(string newStatus)` – changes status with validation
  - `Display()` – prints basic details

### IReportable Interface
Defines methods for all issue types:
- `Display()`
- `ReportStatus()`
- `GetSummary()`
- `AdvanceStatus(string newStatus)`

####  Bug Class
- Field: `Severity` (e.g., High)
- Overrides `Display()`
- Implements `ReportStatus()` and `GetSummary()`

#### Task
- Field: `EstimatedHours`
- Implements `ReportStatus()` and `GetSummary()`

####  FeatureRequest Class
- Fields: `RequestedBy`, `PlannedReleaseDate`
- Implements `ReportStatus()` and `GetSummary()`

---

##  Optional Enhancement
- Counts and displays how many issues are in **Open**, **In Progress**, and **Closed** states

---

### How to Run the Project

### Prerequisites

- .NET SDK installed (version 6 or later recommended)

###  Steps

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

<img width="598" height="533" alt="Screenshot 2025-07-23 142731" src="https://github.com/user-attachments/assets/5fde3f52-70c1-408a-982f-aeb4742c1f8c" />


 ---

