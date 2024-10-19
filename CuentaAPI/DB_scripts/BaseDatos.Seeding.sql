USE cuentas_servicio;
GO

SET IDENTITY_INSERT tipos_cuentas ON;

-- Tipos de cuentas

IF NOT EXISTS ( SELECT 1 FROM tipos_cuentas WHERE descripcion = 'Ahorros')
BEGIN
    INSERT INTO tipos_cuentas (Id, descripcion) VALUES (1, 'Ahorros');
END

IF NOT EXISTS ( SELECT 1 FROM tipos_cuentas WHERE descripcion = 'Corriente')
BEGIN
    INSERT INTO tipos_cuentas (Id, descripcion) VALUES (2, 'Corriente');
END

SET IDENTITY_INSERT tipos_cuentas OFF;

-- Tipos de movimientos

SET IDENTITY_INSERT tipos_movimientos ON;

IF NOT EXISTS ( SELECT 1 FROM tipos_movimientos WHERE descripcion = 'Deposito')
BEGIN
    INSERT INTO tipos_movimientos (Id, descripcion) VALUES (1, 'Deposito');
END

IF NOT EXISTS ( SELECT 1 FROM tipos_movimientos WHERE descripcion = 'Retiro')
BEGIN
    INSERT INTO tipos_movimientos (Id, descripcion) VALUES (2, 'Retiro');
END

SET IDENTITY_INSERT tipos_movimientos OFF;
