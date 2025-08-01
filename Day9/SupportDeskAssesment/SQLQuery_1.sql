-- CREATE DATABASE BugTrackerLiteDB;
-- GO


-- USE BugTrackerLiteDB;
-- GO


-- CREATE TABLE Users (
--     UserId INT PRIMARY KEY IDENTITY(1,1),
--     UserName NVARCHAR(100) NOT NULL
-- );

-- CREATE TABLE Tickets (
--     TicketId INT PRIMARY KEY IDENTITY(1,1),
--     Title NVARCHAR(200),
--     Description NVARCHAR(MAX),
--     Status NVARCHAR(50),
--     CreatedDate DATETIME,
--     UserId INT,
--     FOREIGN KEY (UserId) REFERENCES Users(UserId)
-- );

-- CREATE TABLE Tags (
--     TagId INT PRIMARY KEY IDENTITY(1,1),
--     TagName NVARCHAR(100) NOT NULL
-- );
SELECT * from Users;

