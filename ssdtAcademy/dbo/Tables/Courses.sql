CREATE TABLE [dbo].[Courses] (
    [Code]  NVARCHAR (50)  NOT NULL,
    [Title] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.Courses] PRIMARY KEY CLUSTERED ([Code] ASC)
);

