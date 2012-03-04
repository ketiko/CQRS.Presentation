BEGIN TRAN

select * from Person.Contact c
join HumanResources.Employee e
on c.ContactID = e.ContactID
join HumanResources.EmployeeAddress a
on a.EmployeeID = e.EmployeeID
join Person.Address pa
on pa.AddressID = a.EmployeeID
join Person.StateProvince s  
on s.StateProvinceId = pa.StateProvinceId
where s.CountryRegionCode = 'US'
and s.StateProvinceCode = 'TN'

ROLLBACK