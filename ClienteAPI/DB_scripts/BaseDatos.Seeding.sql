USE clientes_servicio;
GO

SET IDENTITY_INSERT tipos_generos ON;

-- Tipos de Generos

IF NOT EXISTS ( SELECT 1 FROM tipos_generos WHERE descripcion = 'Masculino')
BEGIN
    INSERT INTO tipos_generos (Id, descripcion) VALUES (1, 'Masculino');
END

IF NOT EXISTS ( SELECT 1 FROM tipos_generos WHERE descripcion = 'Femenino')
BEGIN
    INSERT INTO tipos_generos (Id, descripcion) VALUES (2, 'Femenino');
END

SET IDENTITY_INSERT tipos_generos OFF;
