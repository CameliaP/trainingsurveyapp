CREATE PROCEDURE [dbo].seedTrainings
	
AS
	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) values(
	'Apple adv course-Jan', '01/02/2015', '01/04/2015', 'APP1020')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) values(
	'Apple adv course-feb', '02/02/2015', '02/05/2015', 'APP1020')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) values(
	'Apple adv course-march', '03/02/2015', '03/05/2015', 'APP1020')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) values(
	'Apple adv course-april', '04/15/2015', '01/19/2015', 'APP1020')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) values(
	'Apple adv course-may', '05/20/2015', '01/25/2015', 'AGL1015')


	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'BDD using Jbehave-Jan', '01/02/2015', '01/04/2015', 'AGL1015')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'BDD using Jbehave-feb', '02/02/2015', '02/05/2015', 'AGL1015')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'BDD using Jbehave-march', '03/02/2015', '03/05/2015', 'AGL1015')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'TERADATA_DB135-april', '04/15/2015', '01/19/2015', 'AGL1015')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'TERADATA_DB135-may', '05/20/2015', '01/25/2015', 'AGL1015')


	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'TERADATA_DB135-Jan', '01/02/2015', '01/04/2015', 'DB135')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'TERADATA_DB135-feb', '02/02/2015', '02/05/2015', 'DB135')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'TERADATA_DB135-march', '03/02/2015', '03/05/2015', 'DB135')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'TERADATA_DB135-april', '04/15/2015', '01/19/2015', 'DB135')

	insert into dbo.Trainings([Title],[Start], [End], [Course_Code]) 
	values(
	'TERADATA_DB135-may', '05/20/2015', '01/25/2015', 'AGL1015')
RETURN 0