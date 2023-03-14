#!/bin/bash
 
 ./app/docker-db-init.sh $1 $2 $3 $4 & /opt/mssql/bin/sqlservr