BEGIN
	INSERT INTO dbo.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
		(NEWID(), dbo.fnQuestionId('Does fire need oxygen?'), dbo.fnAnswerId('Yes'), 1),
		(NEWID(), dbo.fnQuestionId('Does fire need oxygen?'), dbo.fnAnswerId('No'), 0),

		(NEWID(), dbo.fnQuestionId('What color is grass?'), dbo.fnAnswerId('Green'), 1),
		(NEWID(), dbo.fnQuestionId('What color is grass?'), dbo.fnAnswerId('Red'), 0),
		(NEWID(), dbo.fnQuestionId('What color is grass?'), dbo.fnAnswerId('Blue'), 0),

		(NEWID(), dbo.fnQuestionId('Which season does Christmas take place in?'), dbo.fnAnswerId('Spring'), 0),
		(NEWID(), dbo.fnQuestionId('Which season does Christmas take place in?'), dbo.fnAnswerId('Summer'), 0),
		(NEWID(), dbo.fnQuestionId('Which season does Christmas take place in?'), dbo.fnAnswerId('Fall'), 0),
		(NEWID(), dbo.fnQuestionId('Which season does Christmas take place in?'), dbo.fnAnswerId('Winter'), 1),

		(NEWID(), dbo.fnQuestionId('Which state is FVTC in?'), dbo.fnAnswerId('Ohio'), 0),
		(NEWID(), dbo.fnQuestionId('Which state is FVTC in?'), dbo.fnAnswerId('Michigan'), 0),
		(NEWID(), dbo.fnQuestionId('Which state is FVTC in?'), dbo.fnAnswerId('Florida'), 0),
		(NEWID(), dbo.fnQuestionId('Which state is FVTC in?'), dbo.fnAnswerId('Wisconsin'), 1),

		(NEWID(), dbo.fnQuestionId('Which state is DisneyWorld in?'), dbo.fnAnswerId('Ohio'), 0),
		(NEWID(), dbo.fnQuestionId('Which state is DisneyWorld in?'), dbo.fnAnswerId('Michigan'), 0),
		(NEWID(), dbo.fnQuestionId('Which state is DisneyWorld in?'), dbo.fnAnswerId('Florida'), 1),
		(NEWID(), dbo.fnQuestionId('Which state is DisneyWorld in?'), dbo.fnAnswerId('Wisconsin'), 0)
END