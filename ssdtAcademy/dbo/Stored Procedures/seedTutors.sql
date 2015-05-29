CREATE PROCEDURE [dbo].seedTutors
	
AS
	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('Niranjan Vijay Awati', 'niranjan_awati', 41993)
	insert into dbo.Tutors([Id]) values(41993)

	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('Ruchira Agarwal', 'ruchira_agarwal', 106456)
	insert into dbo.Tutors([Id]) values(106456)

	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('A Victor Sundararaj', 'victor_s', 96488)
	insert into dbo.Tutors([Id]) values(96488)

	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('Lakshmi Lakshmana Rao', 'Lakshmi_Rao', 113959)
	insert into dbo.Tutors([Id]) values(113959)

	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('Pradeep Vasudeva', 'Pradeep_Vasudeva', 41149)
	insert into dbo.Tutors([Id]) values(41149)

	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('Shivakumar A J', 'Shivakumar_AJ', 57271)
	insert into dbo.Tutors([Id]) values(57271)

	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('Sridhar Gatpa', 'Sridhar_Gatpa', 144460)
	insert into dbo.Tutors([Id]) values(144460)

	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('tomy_thomas', 'tomy_thomas', 10206)
	insert into dbo.Tutors([Id]) values(10206)

	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('Neena Mary', 'neena_mc', 8037)
	insert into dbo.Tutors([Id]) values(8037)

	insert into dbo.Employees([Alias], [Email], [Id]) 
	values('Srividhya P', 'srividhya_p', 9687)
	insert into dbo.Tutors([Id]) values(9687)
RETURN 0