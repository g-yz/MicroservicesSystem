USE clients_service;
GO

SET IDENTITY_INSERT type_genders ON;

-- Types of Gender

IF NOT EXISTS ( SELECT 1 FROM type_genders WHERE description = 'Masculine')
BEGIN
    INSERT INTO type_genders (Id, description) VALUES (1, 'Masculine');
END

IF NOT EXISTS ( SELECT 1 FROM type_genders WHERE description = 'Feminine')
BEGIN
    INSERT INTO type_genders (Id, description) VALUES (2, 'Feminine');
END

SET IDENTITY_INSERT type_genders OFF;
