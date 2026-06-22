CREATE DATABASE EmployeeDB;
GO

USE EmployeeDB;
GO

CREATE TABLE Employees
(
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    DOB DATE,
    Country VARCHAR(100),
    State VARCHAR(100),
    City VARCHAR(100),
    Qualification VARCHAR(100),
    Email VARCHAR(100),
    PhoneNo VARCHAR(20)
);
GO
