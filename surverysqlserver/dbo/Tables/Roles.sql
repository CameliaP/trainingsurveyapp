CREATE TABLE [dbo].[Roles] (
    [Code]  NVARCHAR (10) NOT NULL,
    [Title] NVARCHAR (10) NULL,
    CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED ([Code] ASC)
);

