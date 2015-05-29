CREATE TABLE [dbo].[Locations] (
    [Code]  NVARCHAR (50)  NOT NULL,
    [Title] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.Locations] PRIMARY KEY CLUSTERED ([Code] ASC)
);

