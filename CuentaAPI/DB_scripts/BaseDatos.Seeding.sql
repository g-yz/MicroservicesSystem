USE cuentas_servicio;
GO

SET IDENTITY_INSERT tipos_cuentas ON;

IF NOT EXISTS ( SELECT 1 FROM tipos_cuentas WHERE descripcion = 'Ahorros')
BEGIN
    INSERT INTO tipos_cuentas (Id, descripcion) VALUES (1, 'Ahorros');
END

IF NOT EXISTS ( SELECT 1 FROM tipos_cuentas WHERE descripcion = 'Corriente')
BEGIN
    INSERT INTO tipos_cuentas (Id, descripcion) VALUES (2, 'Corriente');
END

SET IDENTITY_INSERT tipos_cuentas OFF;
