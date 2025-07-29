DROP DATABASE IF EXISTS BugTrackerDB;
CREATE DATABASE BugTrackerDB;
USE BugTrackerDB;
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    UserRole NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Projects (
    ProjectId INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255)
);
GO

CREATE TABLE Status (
    StatusId INT IDENTITY(1,1) PRIMARY KEY,
    StatusName NVARCHAR(50) NOT NULL UNIQUE
);


CREATE TABLE Bugs (
    BugId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    ProjectId INT FOREIGN KEY REFERENCES Projects(ProjectId),
    AssignedTo INT FOREIGN KEY REFERENCES Users(UserId),
    ReportedBy INT FOREIGN KEY REFERENCES Users(UserId),
    StatusId INT FOREIGN KEY REFERENCES Status(StatusId),
    CreatedDate DATETIME DEFAULT GETDATE()
);



INSERT INTO Users (UserName, UserRole)
VALUES 
('Ram', 'Developer'),
('Swetha', 'Tester'),
('Srinath', 'Project Manager'),
('Diya', 'Developer');


INSERT INTO Projects (ProjectName, Description)
VALUES 
('Inventory System', 'Manages stock levels'),
('Bug Tracker App', 'Tracks bugs and issues');


INSERT INTO Status (StatusName)
VALUES 
('New'),
('In Progress'),
('Resolved'),
('Closed');



INSERT INTO Bugs (Title, Description, ProjectId, AssignedTo, ReportedBy, StatusId)
VALUES
('Login not working', 'Error on login with valid credentials', 1, 1, 2, 1),
('UI misalignment', 'Alignment issue in dashboard', 2, 4, 2, 2),
('Data not saving', 'Save button not working', 1, 1, 2, 1),
('App crash on load', 'Crashes when launching', 2, 4, 3, 1),
('Status not updating', 'Bug status doesn’t change', 1, 1, 2, 3);



SELECT * FROM Users;
SELECT * FROM Projects;
SELECT * FROM Status;
SELECT * FROM Bugs;