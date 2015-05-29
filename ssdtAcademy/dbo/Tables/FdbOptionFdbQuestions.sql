CREATE TABLE [dbo].[FdbOptionFdbQuestions] (
    [FdbOption_Id]   INT NOT NULL,
    [FdbQuestion_Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.FdbOptionFdbQuestions] PRIMARY KEY CLUSTERED ([FdbOption_Id] ASC, [FdbQuestion_Id] ASC),
    CONSTRAINT [FK_dbo.FdbOptionFdbQuestions_dbo.FdbOptions_FdbOption_Id] FOREIGN KEY ([FdbOption_Id]) REFERENCES [dbo].[FdbOptions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.FdbOptionFdbQuestions_dbo.FdbQuestions_FdbQuestion_Id] FOREIGN KEY ([FdbQuestion_Id]) REFERENCES [dbo].[FdbQuestions] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FdbOption_Id]
    ON [dbo].[FdbOptionFdbQuestions]([FdbOption_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FdbQuestion_Id]
    ON [dbo].[FdbOptionFdbQuestions]([FdbQuestion_Id] ASC);

