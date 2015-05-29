CREATE TABLE [dbo].[FdbQuestionTrainings] (
    [FdbQuestion_Id] INT NOT NULL,
    [Training_Id]    INT NOT NULL,
    CONSTRAINT [PK_dbo.FdbQuestionTrainings] PRIMARY KEY CLUSTERED ([FdbQuestion_Id] ASC, [Training_Id] ASC),
    CONSTRAINT [FK_dbo.FdbQuestionTrainings_dbo.FdbQuestions_FdbQuestion_Id] FOREIGN KEY ([FdbQuestion_Id]) REFERENCES [dbo].[FdbQuestions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.FdbQuestionTrainings_dbo.Trainings_Training_Id] FOREIGN KEY ([Training_Id]) REFERENCES [dbo].[Trainings] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FdbQuestion_Id]
    ON [dbo].[FdbQuestionTrainings]([FdbQuestion_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Training_Id]
    ON [dbo].[FdbQuestionTrainings]([Training_Id] ASC);

