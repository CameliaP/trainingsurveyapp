CREATE TABLE [dbo].[SessionQuestions] (
    [Session_Id]  INT NOT NULL,
    [Question_Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.SessionQuestions] PRIMARY KEY CLUSTERED ([Session_Id] ASC, [Question_Id] ASC),
    CONSTRAINT [FK_dbo.SessionQuestions_dbo.Questions_Question_Id] FOREIGN KEY ([Question_Id]) REFERENCES [dbo].[Questions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.SessionQuestions_dbo.Sessions_Session_Id] FOREIGN KEY ([Session_Id]) REFERENCES [dbo].[Sessions] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Session_Id]
    ON [dbo].[SessionQuestions]([Session_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Question_Id]
    ON [dbo].[SessionQuestions]([Question_Id] ASC);

