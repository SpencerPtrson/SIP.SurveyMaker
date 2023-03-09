BEGIN
	INSERT INTO dbo.tblResponse(Id, QuestionId, AnswerId, ResponseDate)
	VALUES
		(NEWID(), dbo.fnQuestionId('Does fire need oxygen?'), dbo.fnAnswerId('Yes'), GETDATE()),
		(NEWID(), dbo.fnQuestionId('Does fire need oxygen?'), dbo.fnAnswerId('No'), GETDATE()),
		(NEWID(), dbo.fnQuestionId('What color is grass?'), dbo.fnAnswerId('Red'), GETDATE()),
		(NEWID(), dbo.fnQuestionId('What color is grass?'), dbo.fnAnswerId('Green'), GETDATE()),
		(NEWID(), dbo.fnQuestionId('Does fire need oxygen?'), dbo.fnAnswerId('Ohio'), GETDATE()),
		(NEWID(), dbo.fnQuestionId('Which state is FVTC in?'), dbo.fnAnswerId('Wisconsin'), GETDATE())
END