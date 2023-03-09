BEGIN
	INSERT INTO dbo.tblActivation(Id, QuestionId, StartDate, EndDate, ActivationCode)
	VALUES
		(NEWID(), dbo.fnQuestionId('Does fire need oxygen?'), '2001-01-01 11:01:01', '2001-12-31 11:01:01', 'DFNON1'),
		(NEWID(), dbo.fnQuestionId('What color is grass?'), '2002-01-01 11:01:01', '2002-12-31 11:01:01', 'WCIGN2'),
		(NEWID(), dbo.fnQuestionId('Which state is FVTC in?'), '2003-01-01 11:01:01', '2003-12-31 11:01:01', 'WSIFI3')
END