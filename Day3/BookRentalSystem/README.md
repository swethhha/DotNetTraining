# Day 3 - Book Rental System (SOLID Principles)

### 📝 Abstract:
This is a **C# Console-based Book Rental System** designed with **SOLID principles**, ensuring a clean and maintainable architecture. It incorporates **abstract classes**, **interfaces**, **a service layer**, and **polymorphism** to handle different book types and generate reports.

---

### 📂 Folder Structure:

| Folder     | Description |
|------------|-------------|
| Models     | Contains `Book` (abstract class), `Fiction`, `NonFiction`, and `IReportable` interface. |
| Services   | Contains `IBookService` interface and `BookService` implementation class. |
| Program.cs | Main entry point: creates book objects, uses service to display books and generate reports. |

---

### 🧑‍💻 Components:

#### 🔹 Models

- **Book** (abstract class)  
  Base class with properties: `Id`, `Title`, `Author`.

- **Fiction** (inherits Book)  
  Adds `Genre` and implements `IReportable`.

- **NonFiction** (inherits Book)  
  Adds `Subject` and implements `IReportable`.

- **IReportable** (interface)  
  Declares `string GenerateReport()` for polymorphic reporting.

#### 🔹 Services

- **IBookService** (interface)  
  Declares:
  - `void DisplayAll(List<Book> books)`
  - `void ShowReports(List<IReportable> reportables)`

- **BookService** (class)  
  Implements `IBookService` and handles the logic to display books and their reports.

---

### ✅ Features:

- Abstract base class for reusable book structure.
- Interface-based reporting with `IReportable`.
- Service interface and implementation (`IBookService`, `BookService`).
- Polymorphic behavior for generating book-specific reports.
- Clean architecture with **SOLID principles**.

---

## 🛠 Technologies Used

- **C#**
- **.NET Console Application**
- **Visual Studio 2022 / VS Code**

---

## 🧭 Steps

1. **Navigate to project directory:**
   ```bash
   cd path\\to\\BookRentalSystem
   
2. **Build the project:**

   ```bash
    dotnet build
2. **Run the application:**

   ```bash
    dotnet run

---
## Sample Output:

  <img width="705" height="447" alt="Screenshot 2025-07-24 153009" src="https://github.com/user-attachments/assets/b8ad7352-f72d-4a95-93f3-5be19fe2a118" />



---

### 🧑‍💻 Author

**Swetha T**  
.NET Training – Day 3

---

### 📜 License

This project is for **educational and demonstration purposes only**.

