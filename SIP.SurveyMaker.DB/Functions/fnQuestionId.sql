﻿CREATE FUNCTION fnQuestionId (@Question VARCHAR(50))
	RETURNS UNIQUEIDENTIFIER AS BEGIN
		RETURN (SELECT Id FROM dbo.tblQuestion WHERE Question=@Question)
END