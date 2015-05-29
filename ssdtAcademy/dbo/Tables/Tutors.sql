CREATE TABLE [dbo].[Tutors] (
    [Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.Tutors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Tutors_dbo.Employees_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Employees] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Tutors]([Id] ASC);

