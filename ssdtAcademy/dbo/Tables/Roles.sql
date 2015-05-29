CREATE TABLE [dbo].[Roles] (
    [Title] NVARCHAR (255) NOT NULL,
    [Level] INT            NULL,
    CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED ([Title] ASC)
);

