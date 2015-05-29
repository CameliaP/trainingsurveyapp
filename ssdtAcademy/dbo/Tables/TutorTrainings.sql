CREATE TABLE [dbo].[TutorTrainings] (
    [Tutor_Id]    INT NOT NULL,
    [Training_Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.TutorTrainings] PRIMARY KEY CLUSTERED ([Tutor_Id] ASC, [Training_Id] ASC),
    CONSTRAINT [FK_dbo.TutorTrainings_dbo.Trainings_Training_Id] FOREIGN KEY ([Training_Id]) REFERENCES [dbo].[Trainings] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TutorTrainings_dbo.Tutors_Tutor_Id] FOREIGN KEY ([Tutor_Id]) REFERENCES [dbo].[Tutors] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Tutor_Id]
    ON [dbo].[TutorTrainings]([Tutor_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Training_Id]
    ON [dbo].[TutorTrainings]([Training_Id] ASC);

