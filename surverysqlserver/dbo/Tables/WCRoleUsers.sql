CREATE TABLE [dbo].[WCRoleUsers] (
    [WCRole_Code] NVARCHAR (10) NOT NULL,
    [User_Email]  NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_dbo.WCRoleUsers] PRIMARY KEY CLUSTERED ([WCRole_Code] ASC, [User_Email] ASC),
    CONSTRAINT [FK_dbo.WCRoleUsers_dbo.Users_User_Email] FOREIGN KEY ([User_Email]) REFERENCES [dbo].[Users] ([Email]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.WCRoleUsers_dbo.WCRoles_WCRole_Code] FOREIGN KEY ([WCRole_Code]) REFERENCES [dbo].[WCRoles] ([Code]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_WCRole_Code]
    ON [dbo].[WCRoleUsers]([WCRole_Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_User_Email]
    ON [dbo].[WCRoleUsers]([User_Email] ASC);

