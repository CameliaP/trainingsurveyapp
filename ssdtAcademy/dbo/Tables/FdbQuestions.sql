CREATE TABLE [dbo].[FdbQuestions] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Text]          NVARCHAR (255) NULL,
    [Category_Code] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_dbo.FdbQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.FdbQuestions_dbo.FdbCategories_Category_Code] FOREIGN KEY ([Category_Code]) REFERENCES [dbo].[FdbCategories] ([Code])
);


GO
CREATE NONCLUSTERED INDEX [IX_Category_Code]
    ON [dbo].[FdbQuestions]([Category_Code] ASC);

