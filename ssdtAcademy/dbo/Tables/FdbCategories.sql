CREATE TABLE [dbo].[FdbCategories] (
    [Code]  NVARCHAR (50)  NOT NULL,
    [Title] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.FdbCategories] PRIMARY KEY CLUSTERED ([Code] ASC)
);

