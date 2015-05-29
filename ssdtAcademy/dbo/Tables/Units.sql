CREATE TABLE [dbo].[Units] (
    [Code]  NVARCHAR (50)  NOT NULL,
    [Title] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.Units] PRIMARY KEY CLUSTERED ([Code] ASC)
);

