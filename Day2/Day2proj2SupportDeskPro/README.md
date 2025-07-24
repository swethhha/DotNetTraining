# Day2Proj2 – SupportDeskPro Ticketing System

A console-based **Support Ticketing System** built using **C# and .NET**, applying key OOP principles including **inheritance**, **interfaces**, **polymorphism**, and **modular design**.

---

##  Project Highlights

###  Base Class: `SupportTicket`

Defines common properties and basic behavior for all ticket types.

- **Properties:**
  - `Id`
  - `Title`
  - `Description`
  - `CreatedBy`
  - `Status` (default: `"Open"`)

- **Methods:**
  - `DisplayDetails()` – prints basic ticket information
  - `CloseTicket()` – sets `Status` to `"Closed"`


### Interface: `IReportable`

Implemented by both ticket types to support custom reporting:

- `void ReportStatus()`


### BugReport Class
- Inherits from: `SupportTicket`
- Implements: `IReportable`
- Adds:
  - `Severity` (e.g., "High", "Low")
- Overrides:
  - `DisplayDetails()` – adds severity info
  - Implements `ReportStatus()` – custom message including severity

###  FeatureRequest Class
- Inherits from: `SupportTicket`
- Implements: `IReportable`
- Adds:
  - `RequestedBy`
  - `ETA` (Estimated delivery date)
- Overrides:
  - `DisplayDetails()` – adds request-specific fields
  - Implements `ReportStatus()` – includes ETA and requestor info

---
### How to Run the Project

### Prerequisites

- .NET SDK installed (version 6 or later recommended)

###  Steps

1. **Open terminal and navigate to project directory:**

   
bash
   cd path\\to\\Day2proj2SupportDeskPro

2. **Build the project:**
bash
    dotnet build
2. **Run the application:**

   
bash
    dotnet run

---
## Sample Output:


<img width="584" height="398" alt="image" src="https://github.com/user-attachments/assets/bc4cec34-f0b1-436b-a0be-a601b0a15397" />


 ---
