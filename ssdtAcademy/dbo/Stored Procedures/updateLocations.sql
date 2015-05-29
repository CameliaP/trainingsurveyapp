CREATE PROCEDURE [dbo].updateLocations
	
AS
	--this is for just trace the data state as information
	-- number of missing locations 
	select Count([Emp Base Location]) as 'Count Missing Locations'
	from dbo.missingLocations

	--number of locations in the database
	select Count([Code]) as 'Locations in DB'
	from dbo.Locations
	
	--inserting all the new locations in the locations table
	insert into dbo.Locations([Code], [Title])(
	select [Emp Base Location],[Emp Base City]
	from dbo.missingLocations)

	--number of locations after the update
	select Count([Code]) as 'Locations after Update'
	from dbo.Locations

RETURN 0