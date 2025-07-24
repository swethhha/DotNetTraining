#  Day2Project2 - Support Ticket System

This is a simple **Support Ticket Management Console Application** built using **C# and .NET**. It demonstrates how to manage support agents and support requests (tickets) with features like status tracking, priority, assignment, and more.

---

## ✅ Features

###  SupportAgent Class
- Holds agent info like `AgentId`, `Name`, and `Department`.
- Displays agent details.

### SupportRequest (Ticket) Class
- Automatically assigns a `RequestId`, `Issue`, and a `SupportAgent`.
- Tracks `Status` (`Open` or `Closed`).
- Records `CreatedOn` timestamp using `DateTime.Now`.
- Calculates `ResolutionTimeInHours` upon closing.
- Supports **Priority** levels: `High`, `Medium`, or `Low`.
- Includes methods to:
  - `MarkResolved()` – mark the ticket as resolved.
  - `Reassign(SupportAgent)` – reassign the ticket to another agent.
  - `DisplaySummary()` – show ticket summary including priority and created date.

---
## ▶️ How to Run the Project

### ✅ Prerequisites

- .NET SDK installed (version 6 or later recommended)

### 📌 Steps

1. **Open terminal and navigate to project directory:**

   ```bash
   cd path\\to\\Day1proj2

2. **Build the project:**

   ```bash
    dotnet build
2. **Run the application:**

   ```bash
    dotnet run

---
## Sample Output:

<img width="508" height="512" alt="image" src="https://github.com/user-attachments/assets/9a3d8c10-b0f8-4afe-a54d-0077a191a9a9" />

<img width="330" height="165" alt="image" src="https://github.com/user-attachments/assets/2613d70e-3819-4c1d-91b4-a2921d87393c" />

 ---
