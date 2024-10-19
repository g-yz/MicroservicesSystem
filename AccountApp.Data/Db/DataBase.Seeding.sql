USE accounts_service;
GO

SET IDENTITY_INSERT type_accounts ON;

-- Type of accounts

IF NOT EXISTS ( SELECT 1 FROM type_accounts WHERE description = 'Savings')
BEGIN
    INSERT INTO type_accounts (Id, description) VALUES (1, 'Savings');
END

IF NOT EXISTS ( SELECT 1 FROM type_accounts WHERE description = 'Checking')
BEGIN
    INSERT INTO type_accounts (Id, description) VALUES (2, 'Checking');
END

SET IDENTITY_INSERT type_accounts OFF;

-- Types of movements

SET IDENTITY_INSERT type_movements ON;

IF NOT EXISTS ( SELECT 1 FROM type_movements WHERE description = 'Deposit')
BEGIN
    INSERT INTO type_movements (Id, description) VALUES (1, 'Deposit');
END

IF NOT EXISTS ( SELECT 1 FROM type_movements WHERE description = 'Withdrawal')
BEGIN
    INSERT INTO type_movements (Id, description) VALUES (2, 'Withdrawal');
END

SET IDENTITY_INSERT type_movements OFF;
