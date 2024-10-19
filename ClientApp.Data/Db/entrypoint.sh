#!/bin/bash
sleep 10

echo "Waiting for SQL Server..."
until /opt/mssql-tools18/bin/sqlcmd -S "clientapp.db" -U sa -P "$MSSQL_SA_PASSWORD" -Q "SELECT 1" -N -C; do sleep 10; done

echo "Setting database..."
/opt/mssql-tools18/bin/sqlcmd -S "clientapp.db" -U sa -P "$MSSQL_SA_PASSWORD" -N -C -i /docker-entrypoint-initdb.d/ClientApp.Data/Db/DataBase.sql
/opt/mssql-tools18/bin/sqlcmd -S "clientapp.db" -U sa -P "$MSSQL_SA_PASSWORD" -N -C -i /docker-entrypoint-initdb.d/ClientApp.Data/Db/DataBase.Seeding.sql

echo "Done."
exit 0
