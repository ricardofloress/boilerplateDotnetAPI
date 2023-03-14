#!/bin/bash

#wait for the SQL Server to come up
sleep 30s

echo "running background setup database script..."
#run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S "localhost" -U sa -P $1 -d master -v MSSQL_USER=$2 -v MSSQL_USER_PASSWORD=$3 -v MSSQL_DATABASE_NAME=$4 -i ./app/db-init.sql

