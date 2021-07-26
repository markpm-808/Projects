USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='DvdLibrary')
DROP DATABASE DvdLibrary
GO

CREATE DATABASE DvdLibrary
GO

USE DvdLibrary
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Dvds')
	DROP TABLE Contacts
GO



CREATE TABLE Dvds (
	DvdId int identity(0,1) not null primary key,
	Title nvarchar(50) not null,
	ReleaseYear int null,
	Director nvarchar(50) null,
	Rating nvarchar(10) null,
	Notes varchar(500) null
)


