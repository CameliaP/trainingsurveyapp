CREATE TABLE [dbo].[Employees] (
    [Id]            INT            NOT NULL,
    [Alias]         NVARCHAR (255) NULL,
    [Email]         NVARCHAR (255) NULL,
    [Location_Code] NVARCHAR (50)  NULL,
    [Project_Code]  NVARCHAR (50)  NULL,
    [Role_Title]    NVARCHAR (255) NULL,
    [Unit_Code]     NVARCHAR (50)  NULL,
    CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Employees_dbo.Locations_Location_Code] FOREIGN KEY ([Location_Code]) REFERENCES [dbo].[Locations] ([Code]),
    CONSTRAINT [FK_dbo.Employees_dbo.Projects_Project_Code] FOREIGN KEY ([Project_Code]) REFERENCES [dbo].[Projects] ([Code]),
    CONSTRAINT [FK_dbo.Employees_dbo.Roles_Role_Title] FOREIGN KEY ([Role_Title]) REFERENCES [dbo].[Roles] ([Title]),
    CONSTRAINT [FK_dbo.Employees_dbo.Units_Unit_Code] FOREIGN KEY ([Unit_Code]) REFERENCES [dbo].[Units] ([Code])
);


GO
CREATE NONCLUSTERED INDEX [IX_Location_Code]
    ON [dbo].[Employees]([Location_Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Code]
    ON [dbo].[Employees]([Project_Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Role_Title]
    ON [dbo].[Employees]([Role_Title] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Unit_Code]
    ON [dbo].[Employees]([Unit_Code] ASC);

