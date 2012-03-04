USE AdventureWorks

BEGIN TRAN

SELECT c.FirstName, c.LastName, c.EmailAddress
FROM Person.Contact c
JOIN HumanResources.Employee e
ON c.ContactID = e.ContactID
JOIN HumanResources.EmployeeAddress a
ON a.EmployeeID = e.EmployeeID
JOIN Person.[Address] pa
ON pa.AddressID = a.EmployeeID
JOIN Person.StateProvince s  
ON s.StateProvinceId = pa.StateProvinceId
WHERE s.CountryRegionCode = 'US'

ROLLBACK