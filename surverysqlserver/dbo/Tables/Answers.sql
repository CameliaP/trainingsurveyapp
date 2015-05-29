CREATE TABLE [dbo].[Answers] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Text]        NVARCHAR (255) NULL,
    [Question_Id] INT            NULL,
    [Value]       INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Answers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.FDBAnswers_dbo.FDBQuestions_Question_Id] FOREIGN KEY ([Question_Id]) REFERENCES [dbo].[Questions] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Question_Id]
    ON [dbo].[Answers]([Question_Id] ASC);

