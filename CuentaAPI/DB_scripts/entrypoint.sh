#!/bin/bash
sleep 10

echo "Waiting for SQL Server..."
until /opt/mssql-tools18/bin/sqlcmd -S "cuentadb" -U sa -P "$MSSQL_SA_PASSWORD" -Q "SELECT 1" -N -C; do sleep 10; done

echo "Setting database..."
/opt/mssql-tools18/bin/sqlcmd -S "cuentadb" -U sa -P "$MSSQL_SA_PASSWORD" -N -C -i /docker-entrypoint-initdb.d/CuentaAPI/DB_scripts/BaseDatos.sql
/opt/mssql-tools18/bin/sqlcmd -S "cuentadb" -U sa -P "$MSSQL_SA_PASSWORD" -N -C -i /docker-entrypoint-initdb.d/CuentaAPI/DB_scripts/BaseDatos.Seeding.sql

echo "Done."
exit 0
