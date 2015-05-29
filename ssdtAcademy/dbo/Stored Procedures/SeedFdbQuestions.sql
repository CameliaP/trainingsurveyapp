CREATE PROCEDURE [dbo].SeedFdbQuestions
	
AS
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Length of the courseware', 'Courseware')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Coverage of training topics', 'Courseware')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Tool and techniques used by the instructor', 'Tutor')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Subject knowledge of the instructor', 'Tutor')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Timely communication of the traning schedule', 'PlnSchedInfor')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Orientation information and setting up the context of training', 'PlnSchedInfor')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Training venue and classroom arrangements', 'Logistics')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Traning equipment and webtools (if used)', 'Logistics')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('My level of understanding after the training', 'Overall')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Would you forward recommend this training to your colleagues?', 'Overall')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Was this traning applicable for your role?', 'Overall')
insert into dbo.FdbQuestions([Text],[Category_Code]) values('Overall rating for the training', 'Overall')

RETURN 0
