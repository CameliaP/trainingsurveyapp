CREATE PROCEDURE [dbo].updateProjects
	
AS
insert into dbo.Projects([Code], [CustomerCode])(
select distinct [Project Code], [Customer Code]
from dbo.employeeImport i left join dbo.Projects p
on i.[Project Code] =p.[Code]
where i.[Project Code] is not null
and p.[Code] is null)

--testing if there no missing
select Count (distinct [Project Code]) as MissingProjectCodes
from dbo.employeeImport i left join dbo.Projects p
on i.[Project Code] =p.[Code]
where i.[Project Code] is not null
and p.[Code] is null
RETURN 0
