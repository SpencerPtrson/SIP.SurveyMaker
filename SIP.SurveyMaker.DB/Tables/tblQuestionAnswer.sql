﻿CREATE TABLE [dbo].[tblQuestionAnswer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [QuestionId] UNIQUEIDENTIFIER NOT NULL, 
    [AnswerId] UNIQUEIDENTIFIER NOT NULL, 
    [IsCorrect] BIT NOT NULL
)
