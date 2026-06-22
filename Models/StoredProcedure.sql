CREATE PROCEDURE sp_InsertEmployee
(
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @DOB DATE,
    @Country VARCHAR(100),
    @State VARCHAR(100),
    @City VARCHAR(100),
    @Qualification VARCHAR(100),
    @Email VARCHAR(100),
    @PhoneNo VARCHAR(20)
)
AS
BEGIN
    INSERT INTO Employees
    (
        FirstName,
        LastName,
        DOB,
        Country,
        State,
        City,
        Qualification,
        Email,
        PhoneNo
    )
    VALUES
    (
        @FirstName,
        @LastName,
        @DOB,
        @Country,
        @State,
        @City,
        @Qualification,
        @Email,
        @PhoneNo
    )
END
GO