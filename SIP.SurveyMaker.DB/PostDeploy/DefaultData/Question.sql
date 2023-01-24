BEGIN
	INSERT INTO dbo.tblQuestion (Id, Question)
	VALUES
		(NEWID(), 'Are you currently employed?'),
		(NEWID(), 'What is your opinion of our company?'),
		(NEWID(), 'How likely are you to use our services again?'),
		(NEWID(), 'I feel valued at my company'),
		(NEWID(), 'I feel valued at my company')
END