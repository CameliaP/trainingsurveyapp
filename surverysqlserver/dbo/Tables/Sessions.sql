CREATE TABLE [dbo].[Sessions] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (40) NULL,
    [Start]         DATETIME      NULL,
    [End]           DATETIME      NULL,
    [Trainer_Email] NVARCHAR (50) NULL,
    [Training_Id]   INT           NULL,
    CONSTRAINT [PK_dbo.Sessions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Sessions_dbo.Trainers_Trainer_Email] FOREIGN KEY ([Trainer_Email]) REFERENCES [dbo].[Trainers] ([Email]),
    CONSTRAINT [FK_dbo.Sessions_dbo.Trainings_Training_Id] FOREIGN KEY ([Training_Id]) REFERENCES [dbo].[Trainings] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Trainer_Email]
    ON [dbo].[Sessions]([Trainer_Email] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Training_Id]
    ON [dbo].[Sessions]([Training_Id] ASC);

