CREATE TABLE [dbo].[Trainings] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (255) NULL,
    [Start]       DATETIME       NOT NULL,
    [End]         DATETIME       NOT NULL,
    [Course_Code] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_dbo.Trainings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Trainings_dbo.Courses_Course_Code] FOREIGN KEY ([Course_Code]) REFERENCES [dbo].[Courses] ([Code])
);


GO
CREATE NONCLUSTERED INDEX [IX_Course_Code]
    ON [dbo].[Trainings]([Course_Code] ASC);

