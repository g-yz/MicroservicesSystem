-- SERVICIO DE CLIENTES

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'clientes_servicio')
BEGIN
    CREATE DATABASE clientes_servicio;
END
GO

USE clientes_servicio;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'personas')
BEGIN
	CREATE TABLE personas (
	  id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	  nombres varchar(100) NOT NULL,
	  direccion VARCHAR(255) NOT NULL,
	  telefono VARCHAR(20) NOT NULL,
	  identificacion VARCHAR(20),
	  edad TINYINT CHECK (edad >= 0),
	  genero_id INT
	)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'clientes')
BEGIN
	CREATE TABLE clientes (
	  id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	  password VARCHAR(255) NOT NULL,
	  estado BIT NOT NULL
	)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'generos')
BEGIN
	CREATE TABLE generos (
	  id INT IDENTITY(1,1) PRIMARY KEY,
	  descripcion VARCHAR(50) NOT NULL
	)
END
GO

ALTER TABLE clientes ADD FOREIGN KEY (id) REFERENCES personas (id)
GO

ALTER TABLE personas ADD FOREIGN KEY (genero_id) REFERENCES generos (id)
GO