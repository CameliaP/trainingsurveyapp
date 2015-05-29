CREATE TABLE [dbo].[UserSessions] (
    [Session_Id] INT           NOT NULL,
    [User_Email] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_dbo.UserSessions] PRIMARY KEY CLUSTERED ([User_Email] ASC, [Session_Id] ASC),
    CONSTRAINT [FK_dbo.SessionUsers_dbo.Sessions_Session_Id] FOREIGN KEY ([Session_Id]) REFERENCES [dbo].[Sessions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.SessionUsers_dbo.Users_User_Email] FOREIGN KEY ([User_Email]) REFERENCES [dbo].[Users] ([Email]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Session_Id]
    ON [dbo].[UserSessions]([Session_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_User_Email]
    ON [dbo].[UserSessions]([User_Email] ASC);

