CREATE TABLE Donation(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50),
	Description NVARCHAR(150),
	Courier FLOAT,
	Quantity INT,
	DateOfRegister DATETIME2,
	Status BIT
);

DROP TABLE Donation;

SELECT * FROM Donation;
SELECT * FROM Donation WHERE Id = 2;


INSERT INTO Donation
	(Name, Description, Courier, Quantity, DateOfRegister, Status)
	OUTPUT INSERTED.Id
	VALUES ('Blusa', 'Inverno', 5, 2, '2021-02-23', 0);

INSERT INTO Donation
	(Name, Description, Courier, Quantity, DateOfRegister, Status)
	OUTPUT INSERTED.Id
	VALUES ('Fogão', 'Azul', 100, 1, '2021-02-01', 1);

UPDATE Donation
	SET Name = 'Cadeira', Description = 'bege', Courier = 18, 
	Quantity = 4, DateOfRegister = '2020-02-23', Status = 0
	WHERE Id = 2;

DELETE FROM Donation WHERE Id = 4;