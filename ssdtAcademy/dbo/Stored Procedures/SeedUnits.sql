CREATE PROCEDURE [dbo].SeedUnits
	
AS
	insert into dbo.Units([Code], [Title]) values('ETA', 'Education training & assessment')
	insert into dbo.Units([Code], [Title]) values('ENG', 'Engineering services')
RETURN 0
