CREATE TABLE students (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	lastName VARCHAR(40) NOT NULL,
	firstName VARCHAR(40) NOT NULL,
	thirdName VARCHAR(40) NOT NULL,
	gruppa VARCHAR(10) NOT NULL,
	birthDate DATE NOT NULL
);

INSERT INTO students (lastName, firstName, thirdName, gruppa, birthDate)
VALUES
('Кондратьев', 'Терентий', 'Максимович','ММВ-110','01-01-1999');