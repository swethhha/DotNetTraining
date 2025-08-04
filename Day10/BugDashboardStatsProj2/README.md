# BugDashboardStats  Project2 – Console-Based Bug Tracker

## Abstract

This is a **C# Console Application** built using the **Repository Pattern** to simulate a basic bug tracking system. It allows developers and QA engineers to manage bugs efficiently through a simple in-memory storage. The application provides grouped statistics, filtering, and sorting capabilities on bugs based on `Status`, `Priority`, and `Project`.

---

## Architecture & Features

- **Layered Architecture**:
  - `Core`: Contains Entities and Interfaces
  - `Application`: Contains the BugService business logic
  - `Infrastructure`: Includes Repositories and DTOs
  - `Program.cs`: Console UI

- **Core Functionalities**:
  - View all bugs
  - Filter bugs by `Status`, `Priority`, or `Project`
  - Sort bugs by `CreatedDate`
  - View grouped statistics by `Status`, `Priority`, and `Project`

---

## Project Structure

```
BugDashboardStats/
├── Core/
│   ├── Entities/
│   │   ├── Bug.cs
│   │   ├── User.cs
│   │   └── Project.cs
│   └── Interfaces/
│       └── IBugRepository.cs
├── Application/
│   └── Services/
│       └── BugService.cs
├── Infrastructure/
│   ├── Repositories/
│   │   └── BugRepository.cs
│   └── DTOs/
│       ├── BugDto.cs
│       └── BugGroupedStatsDto.cs
└── Program.cs
```

---

## Steps to Run the Application

1. **Open Terminal / Command Prompt**  
   Navigate to your project folder:

   ```bash
   cd BugDashboardStats
   ```

2. **Build the Project**:

   ```bash
   dotnet build
   ```

3. **Run the Application**:

   ```bash
   dotnet run
   ```

---

## Sample Menu Options

```
=== Bug Dashboard Menu ===
1. View All Bugs
2. Filter by Status / Priority / Project
3. Sort by Created Date
4. Show Grouped Statistics
5. Exit
```

---

## Sample Output

<img width="377" height="607" alt="Screenshot 2025-08-04 183658" src="https://github.com/user-attachments/assets/daff4a07-4a6f-4239-84a0-dc0016f51f46" />
<img width="456" height="420" alt="Screenshot 2025-08-04 183741" src="https://github.com/user-attachments/assets/47420d33-a08b-4a62-9c5c-6dd8af4f920b" />
<img width="456" height="603" alt="Screenshot 2025-08-04 183756" src="https://github.com/user-attachments/assets/8ab95fc2-eabd-443c-b0c3-8427520c6f54" />
<img width="413" height="352" alt="Screenshot 2025-08-04 183808" src="https://github.com/user-attachments/assets/bff6db77-c56d-4154-999a-c0f5196c44ab" />

---

## Author

**Swetha T**  
.NET Training – Day 10
