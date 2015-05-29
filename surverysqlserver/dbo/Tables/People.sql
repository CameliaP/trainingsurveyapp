CREATE TABLE [dbo].[People] (
    [Email]   NVARCHAR (50) NOT NULL,
    [Alias]   NVARCHAR (50) NULL,
    [Contact] NVARCHAR (10) NULL,
    CONSTRAINT [PK_dbo.People] PRIMARY KEY CLUSTERED ([Email] ASC)
);

