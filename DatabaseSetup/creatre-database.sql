-- Create the database
CREATE DATABASE EmployeeCRUDapplication;
GO

-- Use the new database
USE EmployeeCRUDapplication;
GO

-- Create the Employee table
CREATE TABLE [dbo].[Employee](
    [EmployeeID] INT IDENTITY(1,1) PRIMARY KEY,
    [EmpName] VARCHAR(50),
    [EmailID] NVARCHAR(50),
    [Department] VARCHAR(20),
    [Salary] INT
);
GO

---



--If you want to pre-fill some employee data for testing, add this to the bottom of your script:


INSERT INTO Employee (EmpName, EmailID, Department, Salary)
VALUES
('John Doe', 'john@example.com', 'IT', 50000),
('Jane Smith', 'jane@example.com', 'HR', 45000);
