CREATE TABLE [dbo].[FdbResponses] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [Employee_Id] INT NULL,
    [Question_Id] INT NULL,
    [Response_Id] INT NULL,
    [Training_Id] INT NULL,
    CONSTRAINT [PK_dbo.FdbResponses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.FdbResponses_dbo.Employees_Employee_Id] FOREIGN KEY ([Employee_Id]) REFERENCES [dbo].[Employees] ([Id]),
    CONSTRAINT [FK_dbo.FdbResponses_dbo.FdbOptions_Response_Id] FOREIGN KEY ([Response_Id]) REFERENCES [dbo].[FdbOptions] ([Id]),
    CONSTRAINT [FK_dbo.FdbResponses_dbo.FdbQuestions_Question_Id] FOREIGN KEY ([Question_Id]) REFERENCES [dbo].[FdbQuestions] ([Id]),
    CONSTRAINT [FK_dbo.FdbResponses_dbo.Trainings_Training_Id] FOREIGN KEY ([Training_Id]) REFERENCES [dbo].[Trainings] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Employee_Id]
    ON [dbo].[FdbResponses]([Employee_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Question_Id]
    ON [dbo].[FdbResponses]([Question_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Response_Id]
    ON [dbo].[FdbResponses]([Response_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Training_Id]
    ON [dbo].[FdbResponses]([Training_Id] ASC);

