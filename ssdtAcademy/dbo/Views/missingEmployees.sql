CREATE VIEW [dbo].missingEmployees
	AS 
	SELECT * 
	FROM import.Employees i left join dbo.Employees e
	on i.[Emp No]=e.[Id]
	where e.Id is null
	and i.[Emp No] is not null


