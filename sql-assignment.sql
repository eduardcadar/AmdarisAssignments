CREATE DATABASE Microbuze
GO
USE Microbuze
GO
CREATE TABLE Agencies(
	Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	AgencyName NVARCHAR(30) NOT NULL UNIQUE,
	PhoneNumber NVARCHAR(10) NOT NULL
	);
GO
CREATE TABLE AgencyUsers(
	Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Username NVARCHAR(30) NOT NULL UNIQUE,
	Password NVARCHAR(30) NOT NULL,
	PhoneNumber VARCHAR(10),
	IdAgency INT NOT NULL FOREIGN KEY REFERENCES Agencies(Id)
	);
GO
CREATE TABLE RegularUsers(
	Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Username NVARCHAR(30) NOT NULL UNIQUE,
	Password NVARCHAR(30) NOT NULL,
	PhoneNumber VARCHAR(10),
	Firstname NVARCHAR(30) NOT NULL,
	Lastname NVARCHAR(30) NOT NULL
	);
GO
CREATE TABLE Trips(
	Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	IdAgency INT NOT NULL FOREIGN KEY REFERENCES Agencies(Id),
	DepartureLocation NVARCHAR(30) NOT NULL,
	Destination NVARCHAR(30) NOT NULL,
	DepartureTime DATETIME NOT NULL,
	Duration TIME NOT NULL,
	Price FLOAT NOT NULL,
	Seats INT NOT NULL CHECK(Seats > 0)
	);
GO
CREATE TABLE Reservations(
	IdTrip INT NOT NULL FOREIGN KEY REFERENCES Trips(Id),
	IdUser INT NOT NULL FOREIGN KEY REFERENCES RegularUsers(Id),
	Seats INT NOT NULL CHECK(Seats > 0),
	PRIMARY KEY(IdTrip, IdUser)
	);
GO
INSERT INTO Agencies(AgencyName, PhoneNumber) VALUES
('Cedra Tour', '0111111111'),
('Cora Travel', '0111111112'),
('Got Travel', '0111111113'),
('Optimus Trans', '0111111114'),
('Inter-Tour', '0111111115'),
('Trans Polosam', '0111111116'),
('Apetrans', '0111111117'),
('maSSaro', '0111111118'),
('Stegaiu', '0111111119'),
('Balint Trans', '0111111110')
GO
INSERT INTO AgencyUsers(IdAgency, Username, Password, PhoneNumber) VALUES
(1, 'Cedra', 'parolaCedra', '0111111120'),
(2, 'Cora', 'parolaCora', '0111111121'),
(3, 'Got', 'parolaGot', '0111111122'),
(4, 'Optimus', 'parolaOptimus', '0111111123'),
(5, 'Inter', 'parolaInter', '0111111124'),
(6, 'Polosam', 'parolaPolosam', '0111111125'),
(7, 'Apetrans', 'parolaApetrans', '0111111126'),
(8, 'Massaro', 'parolaMassaro', '0111111127'),
(9, 'Stegaiu', 'parolaStegaiu', '0111111128'),
(10, 'Balint', 'parolaBalint', '0111111129')
GO
INSERT INTO RegularUsers(Username, Password, PhoneNumber, Firstname, Lastname) VALUES
('Edi', 'parolaEdi', '0111111130', 'Eduard', 'Lucaci'),
('Adi', 'parolaAdi', '0111111131', 'Adrian', 'Trunchi'),
('Alex', 'parolaAlex', '0111111132', 'Alexandru', 'Danciu'),
('Cornel', 'parolaCornel', '0111111133', 'Cornel', 'Cozarev'),
('Stefan', 'parolaStefan', '0111111134', 'Stefan', 'Butacu'),
('Bogdan', 'parolaBogdan', '0111111135', 'Bogdan', 'Cipleu'),
('Ana', 'parolaAna', '0111111136', 'Ana', 'Popescu'),
('Iulia', 'parolaIulia', '0111111137', 'Iulia', 'Popovici'),
('Cati', 'parolaCati', '0111111138', 'Ecaterina', 'Hriscu'),
('Adela', 'parolaAdela', '0111111139', 'Adela', 'Ciocoiu')
GO
INSERT INTO Trips(IdAgency, DepartureLocation, Destination,
	DepartureTime, Duration, Price, Seats) VALUES
(1, 'Cluj-Napoca', 'Turda', PARSE('4.04.2022 12:20' AS datetime), PARSE('1:00:00' AS time), 17, 25),
(2, 'Cluj-Napoca', 'Targu-Mures', PARSE('5.04.2022 13:00' AS datetime), PARSE('1:30:00' AS time), 30, 18),
(3, 'Bacau', 'Onesti', PARSE('6.04.2022 17:30' AS datetime), PARSE('0:50:00' AS time), 15, 20),
(2, 'Bacau', 'Iasi', PARSE('9.04.2022 16:10' AS datetime), PARSE('01:45:00' AS time), 35, 16),
(4, 'Turda', 'Targu-Mures', PARSE('5.04.2022 14:30' AS datetime), PARSE('01:20:00' AS time), 32, 19),
(2, 'Bacau', 'Vaslui', PARSE('10.04.2022 15:15' AS datetime), PARSE('2:10:00' AS time), 40, 14),
(6, 'Arad', 'Timisoara', PARSE('12.04.2022 8:25' AS datetime), PARSE('00:35:00' AS time), 12, 24),
(7, 'Timisoara', 'Oradea', PARSE('12.04.2022 11:30' AS datetime), PARSE('1:10:00' AS time), 17, 24),
(2, 'Iasi', 'Bacau', PARSE('9.04.2022 09:00' AS datetime), PARSE('01:45:00' AS time), 35, 16),
(3, 'Onesti', 'Bacau', PARSE('7.04.2022 6:30' AS datetime), PARSE('0:50:00' AS time), 15, 20)
GO
INSERT INTO Reservations(IdTrip, IdUser, Seats) VALUES
(1, 2, 3),
(2, 4, 5),
(2, 2, 7),
(10, 5, 2),
(3, 1, 5),
(4, 3, 1),
(5, 1, 2),
(1, 8, 1),
(9, 7, 2),
(7, 2, 4)
GO

-- 1. show trips with agency name, in order of departure time
SELECT A.AgencyName, T.DepartureLocation, T.Destination, T.DepartureTime, T.Duration, T.Price
FROM Trips T
INNER JOIN Agencies A ON T.IdAgency = A.Id
ORDER BY T.DepartureTime

-- 2. show trips with number of seats left
SELECT A.AgencyName, T.DepartureLocation, T.Destination, T.DepartureTime,
	T.Seats - (
		SELECT COALESCE(SUM(R.Seats), 0)
		FROM Reservations R
		WHERE R.IdTrip = T.Id
	) AS 'Seats left'
FROM Trips T
INNER JOIN Agencies A ON T.IdAgency = A.Id

-- 3. trips with no reservations
SELECT T.Id, T.DepartureLocation, T.Destination
FROM Trips T
WHERE NOT EXISTS (
	SELECT NULL
	FROM Reservations R
	WHERE R.IdTrip = T.Id
)

-- 4. trips from/to bacau with less than 20 seats
SELECT T.DepartureLocation, T.Destination, T.DepartureTime, T.Seats
FROM Trips T
WHERE T.Seats < 20 AND (T.DepartureLocation = 'Bacau' OR T.Destination = 'Bacau')

-- 5. trips on 05.10.2022
SELECT T.DepartureLocation, T.Destination, T.DepartureTime
FROM Trips T
WHERE T.DepartureTime > '05.04.2022' AND T.DepartureTime < DATEADD(DAY, 1, '05.04.2022')

-- 6. trips longer than an hour
SELECT T.DepartureLocation, T.Destination, T.DepartureTime, T.Duration
FROM Trips T
WHERE T.Duration > '1:00:00'

-- 7. agencies with name that contains 'Tour'
SELECT A.AgencyName, A.PhoneNumber
FROM Agencies A
WHERE A.AgencyName LIKE '%Tour%'

-- 8. regular users that have more than one reservation
SELECT R.Id, R.Username, R.Firstname, R.Lastname, COUNT(Re.IdUser) AS NoReservations
FROM RegularUsers R
INNER JOIN Reservations Re ON Re.IdUser = R.Id
GROUP BY R.Id, R.Username, R.Firstname, R.Lastname
HAVING COUNT(Re.IdUser) > 1

-- 9. average price of trips with more than 17 seats
SELECT AVG(T.Price) AS 'Average price'
FROM Trips T
WHERE T.Seats > 17

-- 10. for every reservation show the trip with the regular user of the reservation
SELECT T.DepartureLocation, T.Destination, T.DepartureTime, R.Firstname, R.Lastname, Re.Seats AS 'Reserved seats'
FROM Reservations Re
INNER JOIN RegularUsers R ON Re.IdUser = R.Id
INNER JOIN Trips T ON Re.IdTrip = T.Id


BEGIN TRY
	BEGIN TRAN
		UPDATE Trips SET Duration = PARSE('00:40:00' AS time) WHERE Id = 7
		IF @@ROWCOUNT < 1 BEGIN
			RAISERROR('no row updated', 15, 1)
		END

		UPDATE RegularUsers SET Firstname = 'Edi' WHERE Firstname = 'Eduard'
		IF @@ROWCOUNT < 1 BEGIN
			RAISERROR('no row updated', 15, 1)
		END

		UPDATE Agencies SET AgencyName = 'Cedra' WHERE Id = 1
		IF @@ROWCOUNT < 1 BEGIN
			RAISERROR('no row updated', 15, 1)
		END

		DELETE FROM AgencyUsers WHERE Username LIKE '%ss%'
		IF @@ROWCOUNT < 1 BEGIN
			RAISERROR('no row deleted', 15, 1)
		END

		DELETE FROM Reservations WHERE IdTrip = 7 AND IdUser = 2
		IF @@ROWCOUNT < 1 BEGIN
			RAISERROR('no row deleted', 15, 1)
		END
	COMMIT TRAN
END TRY
BEGIN CATCH
	PRINT(ERROR_MESSAGE())
	ROLLBACK TRAN
END CATCH
