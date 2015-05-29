CREATE VIEW [dbo].missingLocations
	AS SELECT distinct i.[Emp Base Location], i.[Emp Base City]
	FROM import.Employees i left join dbo.Locations l
	on i.[Emp Base Location]=l.[Code]
	where l.Code is null
	and i.[Emp Base Location] is not null