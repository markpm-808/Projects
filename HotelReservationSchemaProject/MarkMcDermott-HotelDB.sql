-- Execute in the context of the SQL Server master database.
USE [master];
GO

-- If the database already exists:
-- delete backups
-- terminate existing database connections
-- drop it so we can start over
if exists (select * from sys.databases where name = N'HotelDB')
begin
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'HotelDB';
	ALTER DATABASE HotelDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE HotelDB;
end

-- Here's our original CREATE.
CREATE DATABASE HotelDB;
GO

USE HotelDB;
GO

CREATE TABLE Guest (
	GuestID INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(75) NOT NULL,
	[Address] VARCHAR(100) NOT NULL,
	City VARCHAR(75)  NULL,
	[State] CHAR(2)  NULL,
	Zipcode VARCHAR(10)  NULL,
	PhoneNumber VARCHAR(25) NOT NULL
);

CREATE TABLE Reservation (
	ReservationID INT PRIMARY KEY IDENTITY(1,1),
	RoomNumber INT NOT NULL,
	[Name] VARCHAR(75) NOT NULL,
	Adults INT NOT NULL,
	Children INT NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	TotalRoomCost DECIMAL NOT NULL,
	GuestID INT FOREIGN KEY REFERENCES	Guest(GuestID) NOT NULL
);

CREATE TABLE Room (
	RoomID INT PRIMARY KEY IDENTITY(1,1),
	RoomNumber INT NOT NULL,
	[Type] VARCHAR(25) NOT NULL,
	ADA_Accessible BIT NOT NULL,
	StandardOccupancy INT NOT NULL,
	MaximumOccupancy INT NOT NULL,
	BasePrice DECIMAL NOT NULL,
	ExtraPerson DECIMAL NULL
);

CREATE TABLE RoomReservation (
	ReservationID INT FOREIGN KEY REFERENCES Reservation(ReservationID),
	RoomID INT FOREIGN KEY REFERENCES Room(RoomID)
);


CREATE TABLE Amenity (
	AmenityID INT PRIMARY KEY IDENTITY(1,1),
	AmenityName VARCHAR(25) NOT NULL
);


CREATE TABLE RoomAmenity (
	AmenityID INT FOREIGN KEY REFERENCES Amenity(AmenityID),
	RoomNumber INT NOT NULL
);