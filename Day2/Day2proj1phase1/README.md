#  Day2Proj1Phase1 – IssueTracker (Phase 1)

A simple console-based **Issue Tracker** built using **C# and .NET**, demonstrating core OOP principles like inheritance, interfaces, encapsulation, and basic polymorphism.

---

## Core Features

###  Base Class: `Issue`
- **Fields**:
  - `Id` — unique identifier
  - `Title` — short description
  - `AssignedTo` — responsible person
- **Method**:
  - `Display()` — prints the issue’s `Id`, `Title`, and `AssignedTo`

###  Interface: `IReportable`
- Ensures each issue type can `ReportStatus()`

###  Bug Class
- **Extra Field**: `Severity` (e.g., High, Medium, Low)
- **Methods**:
  - `Display()` — includes severity in output
  - `ReportStatus()` — prints status message

###  Task Class
- **Extra Field**: `EstimatedHours`
- **Methods**:
  - `Display()` — includes estimated hours
  - `ReportStatus()` — prints progress message

---

