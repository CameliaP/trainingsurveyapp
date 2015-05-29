CREATE PROCEDURE [dbo].SeedFdbOptions
	
AS
insert into dbo.FdbOptions( [Text],[Sentiment]) values('Not Applicable', 0)
insert into dbo.FdbOptions( [Text],[Sentiment]) values('Disappointing', 1)
insert into dbo.FdbOptions( [Text],[Sentiment]) values('Needs Improvement', 2)
insert into dbo.FdbOptions( [Text],[Sentiment]) values('Ok', 3)
insert into dbo.FdbOptions( [Text],[Sentiment]) values('Good', 4)
insert into dbo.FdbOptions( [Text],[Sentiment]) values('Impressive', 5)
insert into dbo.FdbOptions( [Text],[Sentiment]) values('Yes', 5)
insert into dbo.FdbOptions( [Text],[Sentiment]) values('No', 0)

RETURN 0