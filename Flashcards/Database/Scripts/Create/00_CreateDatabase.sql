DECLARE @DatabaseName VARCHAR(50) = 'FLASHCARDS'
DECLARE @DatabaseExists INT

SELECT @DatabaseExists = CASE 
	WHEN EXISTS(SELECT * FROM sys.databases WHERE name = @DatabaseName)
		THEN 1
		ELSE 0 
	END

IF @DatabaseExists = 0 
BEGIN
	CREATE DATABASE FLASHCARDS
END

