﻿CREATE FUNCTION fnAnswerId (@Text VARCHAR(50))
	RETURNS UNIQUEIDENTIFIER AS BEGIN
		RETURN (SELECT Id FROM dbo.tblAnswer WHERE Text = @Text)
END