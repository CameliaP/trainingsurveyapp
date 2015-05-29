CREATE TABLE [dbo].[TrainingEmployees] (
    [Training_Id] INT NOT NULL,
    [Employee_Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.TrainingEmployees] PRIMARY KEY CLUSTERED ([Training_Id] ASC, [Employee_Id] ASC),
    CONSTRAINT [FK_dbo.TrainingEmployees_dbo.Employees_Employee_Id] FOREIGN KEY ([Employee_Id]) REFERENCES [dbo].[Employees] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TrainingEmployees_dbo.Trainings_Training_Id] FOREIGN KEY ([Training_Id]) REFERENCES [dbo].[Trainings] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Training_Id]
    ON [dbo].[TrainingEmployees]([Training_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Employee_Id]
    ON [dbo].[TrainingEmployees]([Employee_Id] ASC);

