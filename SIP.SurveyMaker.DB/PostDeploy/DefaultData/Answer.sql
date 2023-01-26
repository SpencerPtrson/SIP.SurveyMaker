BEGIN
	INSERT INTO dbo.tblAnswer (Id, Answer)
	VALUES
		(NEWID(), 'Yes'),
		(NEWID(), 'No'),
		(NEWID(), 'Red'),
		(NEWID(), 'Blue'),
		(NEWID(), 'Green'),
		(NEWID(), 'Spring'),
		(NEWID(), 'Summer'),
		(NEWID(), 'Fall'),
		(NEWID(), 'Winter'),
		(NEWID(), 'Ohio'),
		(NEWID(), 'Michigan'),
		(NEWID(), 'Florida'),
		(NEWID(), 'Wisconsin')
END