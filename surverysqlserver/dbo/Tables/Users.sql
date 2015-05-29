CREATE TABLE [dbo].[Users] (
    [Email]          NVARCHAR (50) NOT NULL,
    [RoleAlloc_Code] NVARCHAR (10) NULL,
    [Site_Code]      NVARCHAR (10) NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([Email] ASC),
    CONSTRAINT [FK_dbo.Users_dbo.People_Email] FOREIGN KEY ([Email]) REFERENCES [dbo].[People] ([Email]),
    CONSTRAINT [FK_dbo.Users_dbo.Roles_RoleAlloc_Code] FOREIGN KEY ([RoleAlloc_Code]) REFERENCES [dbo].[Roles] ([Code]),
    CONSTRAINT [FK_dbo.Users_dbo.Sites_Site_Code] FOREIGN KEY ([Site_Code]) REFERENCES [dbo].[Sites] ([Code])
);


GO
CREATE NONCLUSTERED INDEX [IX_RoleAlloc_Code]
    ON [dbo].[Users]([RoleAlloc_Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Site_Code]
    ON [dbo].[Users]([Site_Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Email]
    ON [dbo].[Users]([Email] ASC);

