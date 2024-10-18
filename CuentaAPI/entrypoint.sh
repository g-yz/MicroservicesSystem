#!/bin/bash

echo "Waiting for SQL Server to start..."
sleep 30s

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P cuentapwd -i BaseDatos.sql
echo "Database created."

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P cuentapwd -i BaseDatos.Seeding.sql
echo "Seeding data."
