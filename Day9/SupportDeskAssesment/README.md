#  Week 2 Assessment – SupportDeskAssessment

---

##  SQL Database: `BugTrackerLiteDB`

### Tables

#### 1. **User**
- `UserId` (Primary Key)
- `Name` (User name)

#### 2. **Ticket**
- `TicketId` (Primary Key)
- `Title`
- `IsResolved` (Boolean status)
- `UserId` (Foreign Key → User)

#### 3. **Tag**
- `TagId` (Primary Key)
- `TagName`

#### 4. **TicketTag** (Join Table – Many-to-Many)
- `TicketId` (Foreign Key → Ticket)
- `TagId` (Foreign Key → Tag)

---

## Technologies Used

- .NET 6 / 7 Console App  
-  Entity Framework Core (EF Core) – Database-First Approach  
-  SQL Server (via Azure Data Studio / SSMS)

---

## NuGet Packages Installed

```bash
Microsoft.EntityFrameworkCore.SqlServer  
Microsoft.EntityFrameworkCore.Tools  
Microsoft.EntityFrameworkCore.Design
```

---

## Scaffold Command Used

```bash
dotnet ef dbcontext scaffold "Server=localhost;Database=BugTrackerLiteDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -f
```

> Replace `YourPassword` and server name as per your SQL Server settings.

---

## Project Functionalities

* Create **Users**, **Tickets**, and **Tags**
* Assign **Tags** to **Tickets** (Many-to-Many)
* View all **Tickets** with associated **Users** and **Tags**
* Mark a **Ticket** as **Resolved**
* Delete **Tickets**, **Tags**, or **Users**
* Uses **Navigation Properties** for efficient querying

---

## Sample Usage Scenarios

* Add a new **User**
* Create a **Ticket** for that user
* Add **Tags** to that ticket
* View all **Tickets** with User and Tag details
* Mark ticket as **Resolved**
* Delete Tag → changes reflected in join table
* Delete User → also removes related tickets

---

## 🚀 Steps to Run the Application

1. **Open Terminal / Command Prompt**

2. Navigate to your project directory:

```bash
cd SupportDeskAssesment
```

3. **Build the project:**

```bash
dotnet build
```

4. **Run the project:**

```bash
dotnet run
```

---

## 📸 Output

<img width="361" height="599" alt="image" src="https://github.com/user-attachments/assets/e5b1a79c-59ff-40ec-ba59-d4b4a4eca7f2" />
<img width="365" height="512" alt="image" src="https://github.com/user-attachments/assets/b10081a3-1b29-4cba-93c3-cd2293be4e39" />
<img width="433" height="535" alt="image" src="https://github.com/user-attachments/assets/b7814c19-3648-426e-aa45-1c19b49b0f9c" />

---

## 👩‍💻 Author

**Swetha T**  
.NET Training – Week 2  
Assessment Project: SupportDeskAssessment
