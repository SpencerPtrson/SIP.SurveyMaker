ALTER TABLE [dbo].[tblQuestionAnswer]
	ADD CONSTRAINT [fkAnswerId]
	FOREIGN KEY (AnswerId)
	REFERENCES [dbo].[tblAnswer] (Id)
