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

### 🚀 Issue Types
#### 🔧 Bug
- Field: `Severity` (e.g., High)
- Overrides `Display()`
- Implements `ReportStatus()` and `GetSummary()`

#### 🛠️ Task
- Field: `EstimatedHours`
- Implements `ReportStatus()` and `GetSummary()`

#### 🌟 FeatureRequest
- Fields: `RequestedBy`, `PlannedReleaseDate`
- Implements `ReportStatus()` and `GetSummary()`

---

## 🔄 Optional Enhancement
- Counts and displays how many issues are in **Open**, **In Progress**, and **Closed** states

---

## ▶️ How to Build & Run

### ✅ Prerequisites
- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)

### 🛠 Steps

```bash
cd path/to/Day2proj1phase2
dotnet build
dotnet run

