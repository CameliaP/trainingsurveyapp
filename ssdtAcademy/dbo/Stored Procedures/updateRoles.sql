CREATE PROCEDURE [dbo].updateRoles

AS
insert into dbo.Roles([Title], [Level])(
select distinct i.[Role Capability], i.[Job Band]
from dbo.employeeImport i left join dbo.Roles r
on i.[Role Capability] =r.[Title]
where r.[Title] is null
and i.[Role Capability] is not null)

--checking now to see if the roles are being added
select i.[Role Capability], i.[Job Band]
from dbo.employeeImport i left join dbo.Roles r
on i.[Role Capability] =r.[Title]
where r.[Title] is null
and i.[Role Capability] is not null
RETURN 0