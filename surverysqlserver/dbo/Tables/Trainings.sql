CREATE TABLE [dbo].[Trainings] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50) NULL,
    [DayDuration] REAL          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Trainings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

