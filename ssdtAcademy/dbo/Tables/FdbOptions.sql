CREATE TABLE [dbo].[FdbOptions] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Text]      NVARCHAR (255) NULL,
    [Sentiment] SMALLINT       NOT NULL,
    CONSTRAINT [PK_dbo.FdbOptions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

