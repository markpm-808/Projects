USE HotelDB;
GO

--1.Write a query that returns a list of reservations that end in July 2023,
--including the name of the guest, the room number(s), and the reservation dates.

SELECT 
	[Name],
	RoomNumber,
	StartDate,
	EndDate
FROM Reservation
WHERE EndDate BETWEEN '7/1/2023' AND '7/31/2023'

--RESULTS:
--Name			RoomNumber	StartDate	EndDate
--Mark McDermott	205		2023-06-28	2023-07-02
--Walter Holaway	204		2023-07-13	2023-07-14
--Wilfred Vise		401		2023-07-18	2023-07-21
--Bettyann Seery	303		2023-07-28	2023-07-29


--2.Write a query that returns a list of all reservations for rooms with a jacuzzi,
--displaying the guest's name, the room number, and the dates of the reservation.

SELECT 
	res.[Name],
	ra.RoomNumber,
	res.StartDate,
	res.EndDate
FROM Reservation res
INNER JOIN RoomReservation rres ON res.ReservationID = rres.ReservationID
INNER JOIN Room r ON rres.RoomID = r.RoomID
INNER JOIN RoomAmenity ra ON r.RoomNumber = ra.RoomNumber
INNER JOIN Amenity a ON ra.AmenityID = a.AmenityID
GROUP BY a.AmenityName, res.Name, ra.RoomNumber, res.StartDate, res.EndDate
HAVING a.AmenityName = 'jacuzzi'
ORDER BY ra.RoomNumber

--RESULTS:
--Name				RoomNumber	StartDate	EndDate
--Karie Yang		201			2023-03-06	2023-03-07
--Bettyann Seery	203			2023-02-05	2023-02-10
--Karie Yang		203			2023-09-13	2023-09-15
--Mark McDermott	205			2023-06-28	2023-07-02
--Wilfred Vise		207			2023-04-23	2023-04-24
--Mack Simmer		301			2023-11-22	2023-11-25
--Walter Holaway	301			2023-04-09	2023-04-13
--Bettyann Seery	303			2023-07-28	2023-07-29
--Bettyann Seery	305			2023-08-30	2023-09-01
--Duane Cullison	305			2023-02-22	2023-02-24
--Mark McDermott	307			2023-03-17	2023-03-20


--3.Write a query that returns all the rooms reserved for a specific guest, including 
--the guest's name, the room(s) reserved, the starting date of the reservation, and how
--many people were included in the reservation. (Choose a guest's name from the existing data.)

SELECT
	r.[Name],
	r.RoomNumber RoomsReserved,
	r.StartDate BeginningOfReservation,
	SUM(r.Adults + r.Children) AS NumberOfPeople
FROM Reservation r
GROUP BY r.[Name], r.RoomNumber, r.StartDate
HAVING r.[Name] = 'Joleen Tison'

--RESULTS:
--Name			RoomsReserved	BeginningOfReservation	NumberOfPeople
--Joleen Tison	206				2023-06-10				2
--Joleen Tison	208				2023-06-10				1


--4.Write a query that returns a list of rooms, reservation ID, and per-room cost for each
--reservation. The results should include all rooms, whether or not there is a reservation
--associated with the room.

SELECT
	r.RoomNumber,
	res.ReservationID,
	res.TotalRoomCost
FROM Room r
LEFT OUTER JOIN RoomReservation rr ON r.RoomID = rr.RoomID
LEFT OUTER JOIN Reservation res ON rr.ReservationID = res.ReservationID

--RESULTS:
--RoomNumber	ReservationID	TotalRoomCost
--201			4				200
--202			7				350
--203			2				1000
--203			21				400
--204			16				185
--205			15				700
--206			12				600
--206			23				450
--207			10				175
--208			13				600
--208			20				150
--301			9				800
--301			24				600
--302			6				925
--302			25				700
--303			18				200
--304			14				185
--305			3				350
--305			19				350
--306			NULL			NULL
--307			5				525
--308			1				300
--401			11				1200
--401			17				1260
--401			22				1200
--402			NULL			NULL


--5.Write a query that returns all the rooms accommodating at least three guests and that
--are reserved on any date in April 2023.

SELECT 
	res.RoomNumber,
	r.MaximumOccupancy,
	res.StartDate,
	res.EndDate
FROM Room r
INNER JOIN RoomReservation rr ON r.RoomID = rr.RoomID
INNER JOIN Reservation res ON rr.ReservationID = res.ReservationID
GROUP BY res.RoomNumber, r.MaximumOccupancy, res.StartDate, res.EndDate
HAVING r.MaximumOccupancy >= 3 AND ((res.StartDate BETWEEN '4/1/23' AND '4/30/23') OR (res.EndDate BETWEEN '4/1/23' AND '4/30/23'))

--RESULTS:
--RoomNumber	MaximumOccupancy	StartDate	EndDate
--301				4				2023-04-09	2023-04-13


--6.Write a query that returns a list of all guest names and the number of reservations
--per guest, sorted starting with the guest with the most reservations and then by the guest's last name.

SELECT
	g.[Name],
	COUNT(r.[Name]) AS NumberOfReservations

FROM Guest g 
INNER JOIN Reservation r ON g.GuestID = r.GuestID
GROUP BY g.[Name]
ORDER BY NumberOfReservations, SUBSTRING(g.[Name],CHARINDEX(' ',g.[Name])+1, 20)

--RESULTS:
--Name					NumberOfReservations
--Zachery Luechtefeld	1
--Duane Cullison		2
--Walter Holaway		2
--Aurore Lipton			2
--Mark McDermott		2
--Maritza Tilton		2
--Joleen Tison			2
--Wilfred Vise			2
--Karie Yang			2
--Bettyann Seery		3
--Mack Simmer			4



--7.Write a query that displays the name, address, and phone number of a guest based on their phone 
--number. (Choose a phone number from the existing data.)

SELECT
	[name],
	[Address],
	PhoneNumber
FROM Guest
WHERE PhoneNumber = '(377) 507-0974'

--RESULTS:
--name				Address						PhoneNumber
--Aurore Lipton		762 Wild Rose Street		(377) 507-0974