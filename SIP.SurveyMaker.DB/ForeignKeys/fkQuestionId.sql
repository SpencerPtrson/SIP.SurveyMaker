ALTER TABLE [dbo].[tblQuestionAnswer]
	ADD CONSTRAINT [fkQuestionId]
	FOREIGN KEY (QuestionId)
	REFERENCES [dbo].[tblQuestion] (Id)
