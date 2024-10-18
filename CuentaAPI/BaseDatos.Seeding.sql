SET IDENTITY_INSERT tipos_cuentas ON;

INSERT INTO tipos_cuentas (Id, descripcion)
VALUES 
    (1, 'Ahorros'),
    (2, 'Corriente');

SET IDENTITY_INSERT tipos_cuentas OFF;
