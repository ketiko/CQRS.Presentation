USE AdventureWorks

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ViewModel].[USEmployee]') AND type in (N'U'))
DROP TABLE [ViewModel].[USEmployee]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ViewModel].[USEmployeeAddress]') AND type in (N'U'))
DROP TABLE [ViewModel].[USEmployeeAddress]

IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'ViewModel')
DROP SCHEMA [ViewModel]

GO

CREATE SCHEMA ViewModel Authorization dbo;

GO

CREATE TABLE ViewModel.USEmployee (
	EmployeeId int PRIMARY KEY NOT NULL,
	FirstName nvarchar(500),
	LastName nvarchar(500),
	EmailAddress nvarchar(500)
);

CREATE TABLE ViewModel.USEmployeeAddress (
	EmployeeId int PRIMARY KEY NOT NULL,
	FirstName nvarchar(500),
	LastName nvarchar(500),
	Street nvarchar(500),
	City nvarchar(500),
	State nvarchar(500)
);

GO

BEGIN TRAN

INSERT INTO ViewModel.USEmployee
SELECT e.EmployeeID, c.FirstName, c.LastName, c.EmailAddress
FROM Person.Contact c
JOIN HumanResources.Employee e
ON c.ContactID = e.ContactID
JOIN HumanResources.EmployeeAddress a
ON a.EmployeeID = e.EmployeeID
JOIN Person.[Address] pa
ON pa.AddressID = a.EmployeeID
JOIN Person.StateProvince s  
ON s.StateProvinceId = pa.StateProvinceId
WHERE s.CountryRegionCode = 'US';

INSERT INTO ViewModel.USEmployeeAddress
SELECT e.EmployeeID, c.FirstName, c.LastName, pa.AddressLine1, pa.City, s.StateProvinceCode
FROM Person.Contact c
JOIN HumanResources.Employee e
ON c.ContactID = e.ContactID
JOIN HumanResources.EmployeeAddress a
ON a.EmployeeID = e.EmployeeID
JOIN Person.[Address] pa
ON pa.AddressID = a.EmployeeID
JOIN Person.StateProvince s  
ON s.StateProvinceId = pa.StateProvinceId
WHERE s.CountryRegionCode = 'US';

COMMIT TRAN