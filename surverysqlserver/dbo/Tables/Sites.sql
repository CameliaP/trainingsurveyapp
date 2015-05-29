CREATE TABLE [dbo].[Sites] (
    [Code]  NVARCHAR (10) NOT NULL,
    [Title] NVARCHAR (50) NULL,
    [Type]  NVARCHAR (20) NULL,
    CONSTRAINT [PK_dbo.Sites] PRIMARY KEY CLUSTERED ([Code] ASC)
);

