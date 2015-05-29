CREATE PROCEDURE [dbo].SeedFdbCategories
	
AS
	insert into dbo.FdbCategories([Code], [Title]) values('Courseware', 'Courseware')
insert into dbo.FdbCategories([Code], [Title]) values('Logistics', 'Logistics')
insert into dbo.FdbCategories([Code], [Title]) values('Overall', 'Overall')
insert into dbo.FdbCategories([Code], [Title]) values('PlnSchedInfor', 'Planning and Scheduling')
insert into dbo.FdbCategories([Code], [Title]) values('Tutor', 'Tutor & Teaching')

RETURN 0
