CREATE PROCEDURE [dbo].updateEmployees
	
AS
	select Count([Emp No]) as 'Missing Employees'
	from import.Employees

	select Count([Id]) as 'Employees present'
	from dbo.Employees

	insert into dbo.Employees(
	[Alias],
	[Email], 
	[Id], 
	[Location_Code], 
	[Project_Code], 
	[Role_Title], 
	[Unit_Code])
	(
		select 
		m.[Emp Name], 
		m.[Emp Mail ID], 
		m.[Emp No], 
		m.[Emp Base Location],
		m.[Project Code],
		m.[Role Capability],
		m.[Emp PU]
		from import.Employees m
	)
	select Count([Id]) as 'After update'
	from dbo.Employees
RETURN 0