#!/bin/bash

echo "Waiting for SQL Server to start..."
sleep 30s

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P cuentapwd -i Database.sql

echo "Database created."
