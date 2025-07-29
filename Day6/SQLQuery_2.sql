
INSERT INTO Users (FullName, Email, Role) VALUES
('Ram Kumar', 'ram@sample.com', 'Developer'),
('Swetha R', 'swetha@sample.com', 'Tester'),
('Srinath M', 'srinath@sample.com', 'Project Manager'),
('Meena P', 'meena@sample.com', 'Developer');


INSERT INTO Projects (ProjectName, StartDate, EndDate) VALUES
('Billing System', '2024-01-01', '2024-06-30'),
('Library App', '2024-03-10', NULL),
('Employee Tracker', '2024-04-15', NULL);


INSERT INTO Status (StatusName) VALUES
('New'),
('In Progress'),
('Resolved'),
('Closed');


INSERT INTO Bugs (Title, Description, Priority, ProjectId, AssignedTo, StatusId) VALUES
('App crashes on login', 'Crash occurs when invalid credentials are entered.', 'High', 1, 1, 1), 
('Search not working', 'Search returns no results on valid input.', 'Medium', 2, 2, 2), 
('Invoice total incorrect', 'Discount calculation wrong for bulk orders.', 'High', 1, 4, 1), 
('UI glitch in dashboard', 'Button overlaps on mobile view.', 'Low', 3, 1, 2), 
('Profile not saving', 'User profile changes are not stored.', 'Medium', 3, 2, 3); 


SELECT * FROM Users;

SELECT * FROM Projects;

SELECT * FROM Status;

SELECT * FROM Bugs;
