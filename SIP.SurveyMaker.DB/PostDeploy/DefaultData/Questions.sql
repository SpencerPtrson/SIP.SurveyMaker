BEGIN
	INSERT INTO dbo.tblQuestion (Id, Text)
	VALUES
		(NEWID(), 'Does fire need oxygen?'),
		(NEWID(), 'What color is grass?'),
		(NEWID(), 'Which season does Christmas take place in?'),
		(NEWID(), 'Which state is FVTC in?'),
		(NEWID(), 'Which state is DisneyWorld in?')
END