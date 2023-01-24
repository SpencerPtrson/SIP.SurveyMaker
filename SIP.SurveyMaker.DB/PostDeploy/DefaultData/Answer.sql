BEGIN
	INSERT INTO dbo.tblAnswer (Id, Answer)
	VALUES
		(NEWID(), 'I am currently employed.'),
		(NEWID(), 'I am not currently employed.'),

		(NEWID(), 'What is your opinion of our company?'),
		(NEWID(), 'Positive opinion'),
		(NEWID(), 'Negative opinion'),
		(NEWID(), 'Ambivalent / Unaware'),



		(NEWID(), 'How likely are you to use our services again?'),
		(NEWID(), 'I feel valued at my company'),


		(NEWID(), 'I feel valued at my company')
END