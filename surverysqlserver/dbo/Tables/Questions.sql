CREATE TABLE [dbo].[Questions] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Text]          NVARCHAR (255) NULL,
    [Type_Code]     NVARCHAR (15)  NULL,
    [Category_Code] NVARCHAR (15)  NULL,
    CONSTRAINT [PK_dbo.Questions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.FDBQuestions_dbo.QCategory_Category_Code] FOREIGN KEY ([Category_Code]) REFERENCES [dbo].[QCategory] ([Code]),
    CONSTRAINT [FK_dbo.FDBQuestions_dbo.QTypes_Type_Code] FOREIGN KEY ([Type_Code]) REFERENCES [dbo].[QTypes] ([Code])
);


GO
CREATE NONCLUSTERED INDEX [IX_Type_Code]
    ON [dbo].[Questions]([Type_Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Category_Code]
    ON [dbo].[Questions]([Category_Code] ASC);

