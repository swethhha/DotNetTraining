
# Week 3 Assessment - EventEase API

- This document describes the structure and development steps for building the EventEase project using ASP.NET Core Web API, EF Core, DTOs, and DataAnnotations for validation.
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
├── EventEase.API/                 --> API Layer (Controllers)
│   ├── Controllers/
│      ├── UserController.cs
│      ├── EventController.cs
│      └── RegistrationController.cs
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
│      ├── EventRequestDTO.cs
│      ├── EventResponseDTO.cs
│      └── RegistrationRequestDTO.cs
│      └── RegistrationResponseDTO.cs
├── EventEase.Infrastructure/     --> Infrastructure Layer (Repositories)
│   └── Repositories/
│       ├── UserRepository.cs
│       ├── EventRepository.cs
│       └── RegistrationRepository.cs

```

---

## API Endpoints

### UserController
- `GET /api/users` → Get all users  
- `GET /api/users/{id}` → Get user by ID  
- `POST /api/users` → Create a new user

### EventController
- `GET /api/events` → Get all events  
- `GET /api/events/{id}` → Get event by ID  
- `POST /api/events` → Create a new event

### RegistrationController
- `GET /api/registrations` → Get all registrations  
- `GET /api/registrations/{id}` → Get registration by ID  
- `POST /api/registrations` → Register a user to an event

---

## Validations (Annotations)

- DTOs use `[Required]`, `[EmailAddress]`, etc. for validation:
  - Users must have valid name and email
  - Events must have name, location, and date
  - Registrations must have valid userId, eventId, and date

---

## Sample Output:
<img width="1164" height="368" alt="image" src="https://github.com/user-attachments/assets/898a8769-f655-4306-95ee-466635ece742" />

<img width="1086" height="590" alt="image" src="https://github.com/user-attachments/assets/8e42b886-e8c1-48f7-8473-5399baebd6e5" />

---

## Instructions to run :

- dotnet build
- dotnet run 
  
- Run the app and navigate to:
```
https://localhost:{port}/swagger
```

---

## Git Commands

```bash
git init
git add .
git commit -m "Initial Commit - EventEase API"
git push -u origin main
