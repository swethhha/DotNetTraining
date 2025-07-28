# Day 4 – Bank Management System(ASSESSMENT)

### Abstract:
This is a **C# Console-based Bank Management System** built using **Single Responsibility Principle (SRP)** and **Interface-based programming**. The system is designed to simulate basic bank operations such as deposit, withdrawal, and transaction tracking, using a layered and organized structure.

---

### Folder Structure:

| Folder      | Description |
|-------------|-------------|
| Interfaces  | Contains `ITransactable` and `IAccountService` interfaces. |
| Models      | Contains abstract `Account`, derived types `SavingsAccount`, `CurrentAccount`, and `Transaction` class. |
| Services    | Contains `AccountService` implementing core banking logic. |
| Program.cs  | Entry point: sets up accounts, performs transactions, and displays results. |

---

### Components:

#### Interfaces

- **ITransactable**
  - Declares: `Deposit(decimal amt)`, `Withdraw(decimal amt)`
  
- **IAccountService**
  - Declares: 
    - `void Display(Account acc)`
    - `void ShowSummary(Account acc)`
    - `void DisplayTransactions(Account acc)`

#### Models

- **Account** (abstract)
  - Properties: `Id`, `Name`, `Balance`, `Type`, `List<Transaction>`
  - Implements: `ITransactable`

- **SavingsAccount / CurrentAccount**
  - Inherit from `Account` and share core transaction logic.

- **Transaction**
  - Holds `Date`, `Amount`, `Type` (Deposit/Withdraw)

####  Services

- **AccountService**
  - Implements `IAccountService`
  - Handles transaction logic and account display formatting.

---

### Features:

- Predefined account and transaction setup.
- Deposit and withdrawal with validation.
- Auto-tracked transaction history.
- Clean display of account and summary.
- Interface-driven, separation of concerns (SRP).
- No external dependencies.

---

## Technologies Used

- **C#**
- **.NET Console Application**
- **Visual Studio 2022 / VS Code**

---

## Steps to Run

1. **Navigate to project directory:**

   ```bash
   cd path\to\Day4Assesment
   
2. **Build the project:**

   ```bash
    dotnet build
2. **Run the application:**

   ```bash
    dotnet run

---
## Sample Output:

  <img width="391" height="306" alt="Screenshot 2025-07-28 192538" src="https://github.com/user-attachments/assets/48ab758d-ccf7-4281-86d5-96e80fcebeb3" />

---

### 🧑‍💻 Author

**Swetha T**  
.NET Training – Day 4

---

### 📜 License

This project is for **educational and demonstration purposes only**.
