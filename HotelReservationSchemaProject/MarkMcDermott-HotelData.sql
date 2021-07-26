USE HotelDB;
GO

INSERT INTO Guest([Name], [Address], City, [State], Zipcode, PhoneNumber) VALUES
	('Mark McDermott', '169 N Kainalu Dr', NULL, NULL, NULL, '808-261-5080'),
	('Mack Simmer',	'379 Old Shore Street',	'Council Bluffs', 'IA',	'51501','(291) 553-0508'),
	('Bettyann Seery','750 Wintergreen Dr.', 'Wasilla','AK','99654','(478) 277-9632'),
	('Duane Cullison',	'9662 Foxrun Lane',	'Harlingen','TX','78552','(308) 494-0198'),
	('Karie Yang',	'9378 W. Augusta Ave.',	'West Deptford','NJ','08096','(214) 730-0298'),
	('Aurore Lipton',	'762 Wild Rose Street',	'Saginaw','MI','48601','(377) 507-0974'),
	('Zachery Luechtefeld',	'7 Poplar Dr.',	'Arvada','CO','80003','(814) 485-2615'),
	('Jeremiah Pendergrass','70 Oakwood St.','Zion','IL','60099','(279) 491-0960'),
	('Walter Holaway','7556 Arrowhead St.',	'Cumberland','RI','02864','(446) 396-6785'),
	('Wilfred Vise','77 West Surrey Street','Oswego','NY','13126','(834) 727-1001'),
	('Maritza Tilton','939 Linda Rd.','Burke','VA',	'22015','(446) 351-6860'),
	('Joleen Tison','87 Queen St.',	'Drexel Hill','PA','19026',	'(231) 893-2755');

INSERT INTO Reservation(RoomNumber, [Name], Adults, Children, StartDate, EndDate, TotalRoomCost,GuestID) VALUES
	(308,'Mack Simmer',	1,	0,	'2/2/2023',	'2/4/2023',	299.98, (SELECT GuestID FROM guest WHERE guest.Name = 'Mack Simmer')),
	(203,'Bettyann Seery',	2,	1,	'2/5/2023',	'2/10/2023',999.95,(SELECT GuestID FROM guest WHERE guest.Name = 'Bettyann Seery')),
	(305,'Duane Cullison',	2,	0,	'2/22/2023','2/24/2023',349.98,(SELECT GuestID FROM guest WHERE guest.Name = 'Duane Cullison')),
	(201,'Karie Yang',	2,	2,	'3/6/2023',	'3/7/2023',	199.99,(SELECT GuestID FROM guest WHERE guest.Name = 'Karie Yang')),
	(307,'Mark McDermott',	1,	1,	'3/17/2023','3/20/2023',524.97,(SELECT GuestID FROM guest WHERE guest.Name = 'Mark McDermott')),
	(302,'Aurore Lipton',	3,	0,	'3/18/2023','3/23/2023',924.95,(SELECT GuestID FROM guest WHERE guest.Name = 'Aurore Lipton')),
	(202,'Zachery Luechtefeld',	2,	2,	'3/29/2023','3/31/2023',349.98,(SELECT GuestID FROM guest WHERE guest.Name = 'Zachery Luechtefeld')),
	(304,'Jeremiah Pendergrass',	2,	0,	'3/31/2023','4/5/2023',874.95,(SELECT GuestID FROM guest WHERE guest.Name = 'Jeremiah Pendergrass')),
	(301,'Walter Holaway',	1,	0,	'4/9/2023',	'4/13/2023',799.96,(SELECT GuestID FROM guest WHERE guest.Name = 'Walter Holaway')),
	(207,'Wilfred Vise',	1,	1,	'4/23/2023','4/24/2023',174.99,(SELECT GuestID FROM guest WHERE guest.Name = 'Wilfred Vise')),
	(401,'Maritza Tilton',	2,	4,	'5/30/2023','6/2/2023',	1199.97,(SELECT GuestID FROM guest WHERE guest.Name = 'Maritza Tilton')),
	(206,'Joleen Tison',	2,	0,	'6/10/2023','6/14/2023',599.96,(SELECT GuestID FROM guest WHERE guest.Name = 'Joleen Tison')),
	(208,'Joleen Tison',	1,	0,	'6/10/2023','6/14/2023',599.96,(SELECT GuestID FROM guest WHERE guest.Name = 'Joleen Tison')),
	(304,'Aurore Lipton',	3,	0,	'6/17/2023','6/18/2023',184.99,(SELECT GuestID FROM guest WHERE guest.Name = 'Aurore Lipton')),
	(205,'Mark McDermott',	2,	0,	'6/28/2023','7/2/2023',	699.96,(SELECT GuestID FROM guest WHERE guest.Name = 'Mark McDermott')),
	(204,'Walter Holaway',	3,	1,	'7/13/2023','7/14/2023',184.99,(SELECT GuestID FROM guest WHERE guest.Name = 'Walter Holaway')),
	(401,'Wilfred Vise',	4,	2,	'7/18/2023','7/21/2023',1259.97,(SELECT GuestID FROM guest WHERE guest.Name = 'Wilfred Vise')),
	(303,'Bettyann Seery',	2,	1,	'7/28/2023','7/29/2023',199.99,(SELECT GuestID FROM guest WHERE guest.Name = 'Bettyann Seery')),
	(305,'Bettyann Seery',	1,	0,	'8/30/2023','9/1/2023',349.98,(SELECT GuestID FROM guest WHERE guest.Name = 'Bettyann Seery')),
	(208,'Mack Simmer',	2,	0,	'9/16/2023','9/17/2023',149.99,(SELECT GuestID FROM guest WHERE guest.Name = 'Mack Simmer')),
	(203,'Karie Yang',	2,	2,	'9/13/2023','9/15/2023',399.98,(SELECT GuestID FROM guest WHERE guest.Name = 'Karie Yang')),
	(401,'Duane Cullison',	2,	2,	'11/22/2023','11/25/2023',1199.97,(SELECT GuestID FROM guest WHERE guest.Name = 'Duane Cullison')),
	(206,'Mack Simmer',	2,	0,	'11/22/2023','11/25/2023',449.97,(SELECT GuestID FROM guest WHERE guest.Name = 'Mack Simmer')),
	(301,'Mack Simmer',	2,	2,	'11/22/2023','11/25/2023',599.97,(SELECT GuestID FROM guest WHERE guest.Name = 'Mack Simmer')),
	(302,'Maritza Tilton',	2,	0,	'12/24/2023','12/28/2023',699.96,(SELECT GuestID FROM guest WHERE guest.Name = 'Maritza Tilton'));
	



INSERT INTO Room (RoomNumber, [Type], ADA_Accessible, StandardOccupancy, MaximumOccupancy, BasePrice, ExtraPerson) VALUES
	(201,'Double',0,	2,	4,	199.99,10),
	(202,'Double',1,	2,	4,	174.99,	10),
	(203,'Double',0,	2,	4,	199.99,	10),
	(204,'Double',1,	2,	4,	174.99,	10),
	(205,'Single',0,	2,	2,	174.99,	NULL),
	(206,'Single',1,	2,	2,	149.99,	NULL),
	(207,'Single',0,	2,	2,	174.99,	NULL),
	(208,'Single',1,	2,	2,	149.99,	NULL),
	(301,'Double',0,	2,	4,	199.99,	10),
	(302,'Double',1,	2,	4,	174.99,	10),
	(303,'Double',0,	2,	4,	199.99,	10),
	(304,'Double',1,	2,	4,	174.99,	10),
	(305,'Single',0,	2,	2,	174.99,	NULL),
	(306,'Single',1,	2,	2,	149.99,	NULL),
	(307,'Single',0,	2,	2,	174.99,	NULL),
	(308,'Single',1,	2,	2,	149.99,	NULL),
	(401,'Suite',1,	3,	8,	399.99,	20),
	(402,'Suite',1,	3,	8,	399.99,	20);


INSERT INTO RoomReservation (ReservationID, RoomID)
SELECT Reservation.ReservationID, Room.RoomID FROM Reservation INNER JOIN Room
ON Reservation.RoomNumber = Room.RoomNumber;


INSERT INTO Amenity (AmenityName) VALUES
	('Microwave'),
	('Jacuzzi'),
	('Refrigerator'),
	('Oven');

INSERT INTO RoomAmenity(RoomNumber, AmenityID) VALUES
	(201,1),
	(201,2),
	(202,3),
	(203,1),
	(203,2),
	(204,3),
	(205,1),
	(205,2),
	(205,3),
	(206,1),
	(206,3),
	(207,1),
	(207,2),
	(207,3),
	(208,1),
	(208,3),
	(301,1),
	(301,2),
	(302,3),
	(303,1),
	(303,2),
	(304,3),
	(305,1),
	(305,2),
	(305,3),
	(306,1),
	(306,3),
	(307,1),
	(307,2),
	(307,3),
	(308,1),
	(308,3),
	(401,1),
	(401,3),
	(401,4),
	(402,1),
	(402,3),
	(402,4);


--Second, after adding all of the data above, create SQL statements that will delete Jeremiah Pendergrass and his reservations from the database.
--Deleting data should start with records that reference Jeremiah Pendergrass using a foreign key and then delete the record from the guest table as the last step.
--The scripts should only delete records related to Jeremiah Pendergrass and his reservations. They should not delete any room data.

DELETE FROM RoomReservation
WHERE ReservationID = (SELECT 
	ReservationID
		FROM Reservation
		WHERE [Name] = 'Jeremiah Pendergrass');

DELETE FROM Reservation
WHERE [Name] = 'Jeremiah Pendergrass';

DELETE FROM Guest
WHERE [Name] = 'Jeremiah Pendergrass';

	
