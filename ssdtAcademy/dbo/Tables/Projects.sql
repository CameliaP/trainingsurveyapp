CREATE TABLE [dbo].[Projects] (
    [Code]         NVARCHAR (50)  NOT NULL,
    [CustomerCode] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.Projects] PRIMARY KEY CLUSTERED ([Code] ASC)
);

