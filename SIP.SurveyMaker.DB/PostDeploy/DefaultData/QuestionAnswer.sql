BEGIN
	INSERT INTO dbo.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Does fire need oxygen?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Yes'), 1),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Does fire need oxygen?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='No'), 0),

		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='What color is grass?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Green'), 1),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='What color is grass?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Red'), 0),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='What color is grass?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Blue'), 0),

		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which season does Christmas take place in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Spring'), 0),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which season does Christmas take place in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Summer'), 0),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which season does Christmas take place in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Fall'), 0),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which season does Christmas take place in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Winter'), 1),

		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which state is FVTC in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Ohio'), 0),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which state is FVTC in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Michigan'), 0),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which state is FVTC in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Florida'), 0),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which state is FVTC in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Wisconsin'), 1),

		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which state is DisneyWorld in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Ohio'), 0),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which state is DisneyWorld in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Michigan'), 0),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which state is DisneyWorld in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Florida'), 1),
		(NEWID(), (SELECT Id FROM dbo.tblQuestion WHERE Question='Which state is DisneyWorld in?'), (SELECT Id FROM dbo.tblAnswer WHERE Answer='Wisconsin'), 0)
END