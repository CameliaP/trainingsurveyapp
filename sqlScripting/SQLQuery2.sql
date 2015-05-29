insert into dbo.courses([Code], [Title])
(select [Course Code],[CourseTitle] from dbo.iLearnImport 
where [Course Code] is not null )
go