USE DvdLibrary
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectAll')
BEGIN
   DROP PROCEDURE DvdSelectAll
END
GO
 
CREATE PROCEDURE DvdSelectAll
AS
 
    SELECT DvdId, Title, ReleaseYear, Director, Rating, Notes
    FROM Dvds
 
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectById')
      DROP PROCEDURE DvdSelectById
GO

CREATE PROCEDURE DvdSelectById (
	@DvdId nvarchar(128)
) AS
BEGIN
	SELECT DvdId, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvds 
	WHERE DvdId = @DvdId
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByTitle')
      DROP PROCEDURE DvdSelectByTitle
GO

CREATE PROCEDURE DvdSelectByTitle (
	@Title nvarchar(128)
) AS
BEGIN
	SELECT DvdId, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvds 
	WHERE Title = @Title
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByYear')
      DROP PROCEDURE DvdSelectByYear
GO

CREATE PROCEDURE DvdSelectByYear (
	@ReleaseYear nvarchar(128)
) AS
BEGIN
	SELECT DvdId, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvds 
	WHERE ReleaseYear = @ReleaseYear
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByDirector')
      DROP PROCEDURE DvdSelectByDirector
GO

CREATE PROCEDURE DvdSelectByDirector (
	@Director nvarchar(128)
) AS
BEGIN
	SELECT DvdId, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvds 
	WHERE Director = @Director
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByRating')
      DROP PROCEDURE DvdSelectByRating
GO

CREATE PROCEDURE DvdSelectByRating (
	@Rating nvarchar(128)
) AS
BEGIN
	SELECT DvdId, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvds 
	WHERE Rating = @Rating
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdInsert')
      DROP PROCEDURE DvdInsert
GO

CREATE PROCEDURE DvdInsert (
	@DvdId int output,
	@Title nvarchar(50),
	@ReleaseYear int,
	@Director nvarchar(50),
	@Rating nvarchar(10),
	@Notes varchar(500)
) AS
BEGIN
	INSERT INTO Dvds (Title, ReleaseYear, Director, Rating, Notes)
	VALUES (@Title, @ReleaseYear, @Director, @Rating, @Notes);

	SET @DvdId = SCOPE_IDENTITY();
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdUpdate')
      DROP PROCEDURE DvdUpdate
GO

CREATE PROCEDURE DvdUpdate (
	@DvdId int,
	@Title nvarchar(50),
	@ReleaseYear int,
	@Director nvarchar(50),
	@Rating nvarchar(10),
	@Notes varchar(500)
) AS
BEGIN
	UPDATE Dvds SET 
		Title = @Title,
		ReleaseYear = @ReleaseYear,
		Director = @Director,
		Rating = @Rating,
		Notes = @Notes
	WHERE DvdId = @DvdId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdDelete')
      DROP PROCEDURE DvdDelete
GO

CREATE PROCEDURE DvdDelete (
	@DvdId int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Dvds WHERE DvdId = @DvdId;

	COMMIT TRANSACTION
END
GO

