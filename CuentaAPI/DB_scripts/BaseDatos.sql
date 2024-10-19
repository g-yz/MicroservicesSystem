-- SERVICIO DE CUENTAS

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'cuentas_servicio')
BEGIN
    CREATE DATABASE cuentas_servicio;
END
GO

USE cuentas_servicio;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tipos_cuentas')
BEGIN
	CREATE TABLE tipos_cuentas (
	  id INT IDENTITY(1,1) PRIMARY KEY,
	  descripcion VARCHAR(50) NOT NULL
	)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tipos_movimientos')
BEGIN
	CREATE TABLE tipos_movimientos (
	  id INT IDENTITY(1,1) PRIMARY KEY,
	  descripcion VARCHAR(50) NOT NULL
	)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'cuentas')
BEGIN
	CREATE TABLE cuentas (
	  id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	  numero_cuenta VARCHAR(20) NOT NULL,
	  saldo_inicial DECIMAL(19,4) NOT NULL DEFAULT 0,
	  tipo_cuenta_id INT NOT NULL,
	  estado BIT NOT NULL,
	  cliente_id UNIQUEIDENTIFIER NOT NULL
	)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'movimientos')
BEGIN
	CREATE TABLE movimientos (
	  id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	  valor DECIMAL(19,4) NOT NULL DEFAULT 0,
	  saldo DECIMAL(19,4) NOT NULL DEFAULT 0,
	  fecha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	  tipo_movimiento_id INT NOT NULL,
	  cuenta_id UNIQUEIDENTIFIER NOT NULL
	)
END
GO

ALTER TABLE cuentas ADD FOREIGN KEY (tipo_cuenta_id) REFERENCES tipos_cuentas (id)
GO

ALTER TABLE movimientos ADD FOREIGN KEY (cuenta_id) REFERENCES cuentas (id)
GO

ALTER TABLE movimientos ADD FOREIGN KEY (tipo_movimiento_id) REFERENCES tipos_movimientos (id)
GO

-- Redundancia

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'clientes')
BEGIN
	CREATE TABLE clientes (
	  id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	  nombres varchar(100) NOT NULL,
	  estado BIT NOT NULL
	)
END
GO

ALTER TABLE cuentas ADD FOREIGN KEY (cliente_id) REFERENCES clientes (id)
GO
