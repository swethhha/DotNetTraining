
# Week 5 Assessment - EventEase API Phase2

- This document describes the **extended structure and development steps** for building the EventEase project in **Phase 2**.  
- The project uses **ASP.NET Core Web API, MVC, EF Core, DTOs, Middleware, Exception Handling** , and **Unit Testing**  .
- This phase builds upon **Phase 1** by adding:
  - A **Global Exception Handling Middleware** for consistent error responses.
  - A **DTO-based error response model** (`ErrorResponseDTO.cs`).
  - **Custom Exceptions** (e.g., `NotFoundException`, `ValidationException`).
  - A **separate MVC Layer** with Razor views for Users, Events, and Registrations.
  - **Unit Testing layer** (`EventEase.Tests`) to test service logic.
---

### Entities:

- **Users**
  - `UserId` 
  - `UserName` 
  - `Email` 

- **Events**
  - `EventId` 
  - `EventName` 
  - `Location` 
  - `EventDate`

- **Registrations**
  - `RegistrationId` 
  - `UserId` 
  - `EventId` 
  - `RegistrationDate` 


---

## Project Structure

```
EventEase/
├── EventEase.API/                 --> API Layer (Controllers,Extensions,Middleware)
│   ├── Controllers/
│      ├── UserController.cs
│      ├── EventController.cs
│      └── RegistrationController.cs
│   ├── Extensions/
│      ├── ExceptionMiddlewareExtensions.cs
│   ├── Middleware/
│      ├── GlobalExceptionMiddleware.cs
│   ├── Program.cs
├── EventEase.Application/        --> Application Layer (Services)
│   └── Services/
│       ├── UserService.cs
│       ├── EventService.cs
│       └── RegistrationService.cs
├── EventEase.Core/               --> Core Layer (Entities, DTOs, Interfaces)
│   ├── Entities/
│      ├── User.cs
│      ├── Event.cs
│      └── Registration.cs
│   └── Interfaces/
│      ├── IUserRepository.cs
│      ├── IEventRepository.cs
│      └── IRegistrationRepository.cs│       
│      ├── IEventService.cs
│      ├── IUserService.cs
│      ├── IRegistrationService.cs
│   ├── DTOs/
│      ├── UserRequestDTO.cs
│      ├── UserResponseDTO.cs
│      ├── ErrorResponseDTO.cs
│      ├── EventRequestDTO.cs
│      ├── EventResponseDTO.cs
│      └── RegistrationRequestDTO.cs
│      └── RegistrationResponseDTO.cs
│   ├── Exceptions/
│      ├── NotFoundException.cs
│      ├── ValidationException.cs
├── EventEase.Infrastructure/     --> Infrastructure Layer (Repositories)
│   └── Repositories/
│       ├── UserRepository.cs
│       ├── EventRepository.cs
│       └── RegistrationRepository.cs
├──  EventEase.MVC/                  --> MVC Layer    
│    ├──  Controllers/                           # MVC controllers
│         ├── EventController.cs
│         └── RegistrationController.cs
│         └── UserController.cs
│         └── HomeController.cs
│    ├──  Models/                                # View models
│         ├── ErrorViewModel.cs
│         ├── EventViewModel.cs
│         ├── RegistrationtionViewModel.cs
│         └── UserViewModel.cs
│   └──  Views/                                 # Razor views
│       ├──  Event/
│       │   ├── Details.cshtml
│       │   └── Index.cshtml
│       ├──  Registration/
│       │   ├── Details.cshtml
│       │   └── Index.cshtml
│       ├──  User/
│       │   ├── Details.cshtml
│       │   └── Index.cshtml
│       └──  Shared/
│           ├── _Layout.cshtml
│           └── Error.cshtml
│
├──  EventEase.Tests/                   #  TESTING LAYER
│    └──  Services/
│           ├── EventServiceTests.cs
│           └── RegistrationServiceTests.cs
│           └── UserServiceTests.cs              
│   └── UnitTest1.cs
└── EventEase.sln 
```


## 👤 UserController

### Sync Endpoints
- `GET /api/users` → Get all users  
- `GET /api/users/{id}` → Get user by ID  
- `POST /api/users` → Create a new user  
- `PUT /api/users/{id}` → Update an existing user  
- `DELETE /api/users/{id}` → Delete a user  

### Async Endpoints
- `GET /api/users/async` → Get all users (async)  
- `GET /api/users/async/{id}` → Get user by ID (async)  
- `POST /api/users/async` → Create a new user (async)  
- `PUT /api/users/async/{id}` → Update an existing user (async)  
- `DELETE /api/users/async/{id}` → Delete a user (async)  

---

## 🎟️ EventController

### Sync Endpoints
- `GET /api/events` → Get all events  
- `GET /api/events/{id}` → Get event by ID  
- `POST /api/events` → Create a new event  
- `PUT /api/events/{id}` → Update an existing event  
- `DELETE /api/events/{id}` → Delete an event  

### Async Endpoints
- `GET /api/events/async` → Get all events (async)  
- `GET /api/events/async/{id}` → Get event by ID (async)  
- `POST /api/events/async` → Create a new event (async)  
- `PUT /api/events/async/{id}` → Update an existing event (async)  
- `DELETE /api/events/async/{id}` → Delete an event (async)  

---

## 📝 RegistrationController

### Sync Endpoints
- `GET /api/registrations` → Get all registrations  
- `GET /api/registrations/{id}` → Get registration by ID  
- `POST /api/registrations` → Register a user to an event  
- `PUT /api/registrations/{id}` → Update a registration  
- `DELETE /api/registrations/{id}` → Delete a registration  

### Async Endpoints
- `GET /api/registrations/async` → Get all registrations (async)  
- `GET /api/registrations/async/{id}` → Get registration by ID (async)  
- `POST /api/registrations/async` → Register a user to an event (async)  
- `PUT /api/registrations/async/{id}` → Update a registration (async)  
- `DELETE /api/registrations/async/{id}` → Delete a registration (async)  

---

## Sample Output:
- ### Get (Async)
<img width="1072" height="532" alt="image" src="https://github.com/user-attachments/assets/7b42203b-3d3f-4ea8-8229-511dae6b914b" />


- ### Post (Async)
<img width="1064" height="221" alt="Screenshot 2025-08-23 192932" src="https://github.com/user-attachments/assets/b996a38d-28a6-4265-901e-790e221b3f92" />


- ### GetById (Async)
<img width="707" height="400" alt="image" src="https://github.com/user-attachments/assets/03193b4a-41b6-4f77-80b6-5dcc4a99bb3e" />

- ### Error Response  
<img width="713" height="512" alt="image" src="https://github.com/user-attachments/assets/67bb3182-d285-4116-a430-e307908bd7e5" />

- ### Put (Async)  
<img width="712" height="264" alt="image" src="https://github.com/user-attachments/assets/163642d4-9a9b-4d9e-a392-9f219f627ed9" />

- ### Delete (Async)  
<img width="1065" height="328" alt="image" src="https://github.com/user-attachments/assets/e8089f6a-c052-459c-908a-8ad63fbe1dd2" />

- ### MVC EventList  
<img width="1358" height="283" alt="image" src="https://github.com/user-attachments/assets/a746ac8e-5540-491f-86db-815cee72be08" />

- ### MVC Details
<img width="710" height="252" alt="image" src="https://github.com/user-attachments/assets/26f8443d-2733-41b1-9a2e-a8539fa17ec7" />

- ### Tests
<img width="690" height="181" alt="image" src="https://github.com/user-attachments/assets/f00c3ead-4f14-481d-934a-e725e9f008fc" />


---

## Instructions to run :
(Set up multiple startup projects - api and mvc)

- dotnet build
- dotnet run 
  
- Run the app and navigate to:
```
https://localhost:{port}/swagger
```
- dotnet test (for tests)
---

## Git Commands

```bash
git init
git add .
git commit -m "Initial Commit - EventEase API Phase 2"
git push -u origin main
