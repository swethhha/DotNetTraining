
# BankPro API - Assessment4

---

## Table of Contents
1. [Project Overview](#project-overview)  
2. [Project Structure](#project-structure)  
3. [Technologies Used](#technologies-used)  
4. [Layers & Functions](#layers--functions)  
5. [API Endpoints](#api-endpoints)  
   - [Account Endpoints](#account-endpoints)  
   - [Customer Endpoints](#customer-endpoints)  
   - [Transaction Endpoints](#transaction-endpoints)  
6. [Screenshots](#screenshots)  
7. [Author](#author)  

---

## Project Overview
BankPro API is a simple banking management RESTful API built with **ASP.NET Core** using **Clean Architecture** (Onion Architecture).  
It supports managing accounts, customers, and transactions. Only read operations are allowed for transactions; account and customer operations allow full CRUD.  

---

## Project Structure

```
BankPro/
│
├── BankPro.API/            # API Layer
│   ├── Controllers/        # All API controllers
│   └── Program.cs
│
├── BankPro.Application/    # Application Layer (Services)
│   ├── Services/
│   │   ├── AccountService.cs
│   │   ├── CustomerService.cs
│   │   └── TransactionService.cs
│
├── BankPro.Core/           # Core Layer (Entities, DTOs, Interfaces)
│   ├── Entities/
│   │   ├── Account.cs
│   │   ├── Customer.cs
│   │   └── Transaction.cs
│   ├── DTOs/
│   │   ├── AccountRequestDTO.cs
│   │   ├── AccountResponseDTO.cs
│   │   ├── CustomerRequestDTO.cs
│   │   ├── CustomerResponseDTO.cs
│   │   └── TransactionRequestDTO.cs
│   │   └── TransactionResponseDTO.cs
│   └── Interfaces/
│       ├── IAccountRepository.cs
│       ├── IAccountService.cs
│       ├── ICustomerRepository.cs
│       ├── ICustomerService.cs
│       ├── IRepository.cs
│       └── ITransactionRepository.cs
│       └── ITransactionService.cs
│
├── BankPro.Infrastructure/ # Infrastructure Layer (Repositories, DB)
│   └── Repositories/
│       ├── AccountRepository.cs
│       ├── CustomerRepository.cs
│       └── TransactionRepository.cs
│
└── BankPro.Tests/
│    ├── Services/ # Unit tests for services using xUnit and Moq
│       ├── AccountServiceTests.cs
│       ├── CustomerServiceTests.cs
│       ├── TransactionServiceTests.cs
│
└── BankPro.sln             # Solution file
```

---

## Technologies Used
- **.NET 8**  
- **C#**  
- **ASP.NET Core Web API**  
- **Entity Framework Core (In-Memory / SQL Server)**  
- **AutoMapper**  
- **Swagger**  

---

## Layers & Functions

### API Layer
- **Controllers** handle HTTP requests, call Services, and return responses.
- Validation and error handling are done here.

### Application Layer
- **Services** contain business logic.
- `AccountService`: CRUD operations, deposit, withdraw, transfer.  
- `CustomerService`: CRUD operations for customers.  
- `TransactionService`: Only read operations; fetch transactions by account, customer, or date.

### Core Layer
- **Entities**: Account, Customer, Transaction.  
- **DTOs**: AccountRequestDTO, CustomerRequestDTO, TransactionResponseDTO.  
- **Interfaces**: Service and repository contracts.  

### Infrastructure Layer
- **Repositories**: Handle data storage (in-memory or database).  

### Test Layer
- Uses **xUnit** and **Moq** for unit testing.
- Tests all service methods:
  - AccountService: Deposit, Withdraw, Transfer, GetAll, GetById
  - CustomerService: CRUD
  - TransactionService: GetAll, GetById, GetByDateRange
    
---

## API Endpoints

### Account Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | /api/Account | Get all accounts |
| GET    | /api/Account/{id} | Get account by Id |
| POST   | /api/Account | Create new account |
| PUT    | /api/Account/{id} | Update existing account |
| DELETE | /api/Account/{id} | Delete account |
| POST   | /api/Account/deposit?accountNumber=&amount= | Deposit money into an account |
| POST   | /api/Account/withdraw?accountNumber=&amount= | Withdraw money from an account |
| POST   | /api/Account/transfer?fromAccount=&toAccount=&amount= | Transfer money between accounts |


### Customer Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | /api/Customer | Get all customers |
| GET    | /api/Customer/{id} | Get customer by Id |
| POST   | /api/Customer | Create new customer |
| PUT    | /api/Customer/{id} | Update customer |
| DELETE | /api/Customer/{id} | Delete customer |

### Transaction Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | /api/Transaction | Get all transactions |
| GET    | /api/Transaction/{id} | Get transaction by Id |
| GET    | /api/Transaction/account/{accountNumber} | Get transactions by account |
| GET    | /api/Transaction/customer/{customerId} | Get transactions by customer |

# Test Layer

- Unit tests are implemented in `BankPro.Tests` using **xUnit** and **Moq**.
- Services tested:
  - **AccountService**: DepositAsync, WithdrawAsync, TransferAsync, CRUD
  - **CustomerService**: CRUD
  - **TransactionService**: GetAllTransactionsAsync, GetTransactionByIdAsync, GetTransactionsByDateRange

---

## Screenshots

### Account Endpoints
- GET All Accounts:  
<img width="498" height="549" alt="Screenshot 2025-08-18 111320" src="https://github.com/user-attachments/assets/351c8d94-9882-4ffa-bba4-b5d45c3d498f" />


- GET Account by Id:  
<img width="691" height="562" alt="Screenshot 2025-08-18 113715" src="https://github.com/user-attachments/assets/7c15d3a2-5e2c-4086-817a-505ea7849cb7" />


- POST Account:  
<img width="277" height="456" alt="Screenshot 2025-08-18 111354" src="https://github.com/user-attachments/assets/05f66150-9fc8-46fc-8b4e-e013758a125c" />


- PUT Account:  
<img width="422" height="508" alt="Screenshot 2025-08-18 113741" src="https://github.com/user-attachments/assets/770a7014-ea3c-41e0-b20d-d1d802326558" />


- DELETE Account:  
<img width="561" height="460" alt="Screenshot 2025-08-18 113958" src="https://github.com/user-attachments/assets/df742fb2-1294-4f91-8dbb-d47f9b0bec97" />


- Transfer Money:  
<img width="569" height="599" alt="Screenshot 2025-08-18 113921" src="https://github.com/user-attachments/assets/8a24d709-3d2d-44b1-a01e-3caf42da4841" />


- Deposit:  
<img width="617" height="514" alt="Screenshot 2025-08-18 113805" src="https://github.com/user-attachments/assets/fe8c5929-19e3-407d-9559-a0f5e392c944" />


- Withdraw:
<img width="577" height="511" alt="Screenshot 2025-08-18 113826" src="https://github.com/user-attachments/assets/bd5ce119-4a5e-4008-91b1-cfd1c6e392de" />


### Customer Endpoints
- GET All Customers:  
  <img width="457" height="404" alt="Screenshot 2025-08-18 114600" src="https://github.com/user-attachments/assets/a464e4c7-49ad-4582-a9a6-3403e31a9a69" />


- GET Customer by Id:  
  <img width="411" height="372" alt="Screenshot 2025-08-18 114616" src="https://github.com/user-attachments/assets/37a6bf58-3995-4bec-879b-1572db09f542" />


- POST Customer:  
  <img width="711" height="558" alt="Screenshot 2025-08-18 114537" src="https://github.com/user-attachments/assets/7ae7acef-6b5a-43c6-b177-4c1a229608c5" />

 
- PUT Customer:  
  <img width="473" height="566" alt="Screenshot 2025-08-18 114649" src="https://github.com/user-attachments/assets/51cef273-f903-4d56-a1ce-948356b63c57" />


- DELETE Customer:  
  <img width="674" height="559" alt="image" src="https://github.com/user-attachments/assets/6493accf-f9e6-416c-8531-a1f335303c3f" />


### Transaction Endpoints
- GET All Transactions:  
  <img width="631" height="621" alt="Screenshot 2025-08-18 122517" src="https://github.com/user-attachments/assets/4c54fd00-6784-4563-a046-ffb89b6efb77" />

 
- GET Transaction by Id:  
  <img width="604" height="500" alt="Screenshot 2025-08-18 122549" src="https://github.com/user-attachments/assets/5b9eca00-6f53-42f2-acad-9695138ef8be" />


- GET Transactions by Type:  
  <img width="514" height="524" alt="Screenshot 2025-08-18 122609" src="https://github.com/user-attachments/assets/e7a618df-5b81-47c4-9418-0a1cbfc9fc73" />


- GET Transactions by Dates:  
  <img width="432" height="631" alt="Screenshot 2025-08-18 122745" src="https://github.com/user-attachments/assets/c8f59a51-f5ba-41c8-b43a-e3f1057f1638" />

### Test Screenshots
- AccountServiceTests, CustomerServiceTests , TransactionServiceTests
  <img width="661" height="133" alt="image" src="https://github.com/user-attachments/assets/ecde112c-63e3-4a52-a21b-3a393d68c41f" />


---

## Author
**Swetha T**  
.Net Assessment 4
