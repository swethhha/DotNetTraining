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
## How to Build and Run

### 📦 Requirements
- [.NET SDK](https://dotnet.microsoft.com/download) (Version 6 or later)

### Build

```bash
dotnet build

### Run the Application

```bash
dotnet run
