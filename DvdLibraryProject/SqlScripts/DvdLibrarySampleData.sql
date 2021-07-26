IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdLibrarySampleData')
      DROP PROCEDURE DvdLibrarySampleData
GO

CREATE PROCEDURE DvdLibrarySampleData AS
BEGIN
	DELETE FROM Dvds;
	
	DBCC CHECKIDENT ('Dvds', RESEED, 0)

	SET IDENTITY_INSERT Dvds ON;

	INSERT INTO Dvds(DvdId, Title, ReleaseYear, Director, Rating, Notes)
	VALUES  (0, 'An old Tale', 2015, 'Sam Jones', 'PG','This really is a great tale!'),
			(1, 'A Good Tale', 2012, 'Joe Smith', 'PG-13','This is a good tale!'),
			(2, 'A New Tale', 2016, 'Sam Jones', 'PG-13','Brand spanking new!'),
			(3, 'An old Tale', 2010, 'Jack Jameson', 'G','This is an old tale!'),
			(4, 'A Secret Tale', 2012, 'Kenny Donald', 'R','This is a secret tale!'),
			(5, 'Batman', 2020, 'Sam Jones', 'PG','Begins!'),
			(6, 'Bats and Pigs', 2012, 'Joe Smith', 'PG-13','The Movie!'),
			(7, 'Legend of Korra', 2015, 'Sam Jones', 'PG','Season 2!'),
			(8, 'League of Legends', 2021, 'Jackson Man', 'PG-13','The Video Game!'),
			(9, 'Legendary staff', 2020, 'Samantha Jacob', 'R','The Legend Begins!');
	 
	SET IDENTITY_INSERT Dvds OFF;

END