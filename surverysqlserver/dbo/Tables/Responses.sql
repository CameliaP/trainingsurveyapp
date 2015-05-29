CREATE TABLE [dbo].[Responses] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Question_Id] INT            NULL,
    [Session_Id]  INT            NULL,
    [User_Email]  NVARCHAR (50)  NULL,
    [Status]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Responses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Responses_dbo.FDBQuestions_Question_Id] FOREIGN KEY ([Question_Id]) REFERENCES [dbo].[Questions] ([Id]),
    CONSTRAINT [FK_dbo.Responses_dbo.Sessions_Session_Id] FOREIGN KEY ([Session_Id]) REFERENCES [dbo].[Sessions] ([Id]),
    CONSTRAINT [FK_dbo.Responses_dbo.Users_User_Email] FOREIGN KEY ([User_Email]) REFERENCES [dbo].[Users] ([Email])
);


GO
CREATE NONCLUSTERED INDEX [IX_Question_Id]
    ON [dbo].[Responses]([Question_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Session_Id]
    ON [dbo].[Responses]([Session_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_User_Email]
    ON [dbo].[Responses]([User_Email] ASC);

