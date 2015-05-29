CREATE TABLE [dbo].[Trainers] (
    [Email] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_dbo.Trainers] PRIMARY KEY CLUSTERED ([Email] ASC),
    CONSTRAINT [FK_dbo.Trainers_dbo.People_Email] FOREIGN KEY ([Email]) REFERENCES [dbo].[People] ([Email])
);


GO
CREATE NONCLUSTERED INDEX [IX_Email]
    ON [dbo].[Trainers]([Email] ASC);

