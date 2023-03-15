USE [master]
GO

IF DB_ID('$(MSSQL_DATABASE_NAME)') IS NOT NULL
  set noexec on               -- prevent creation when already exists

CREATE DATABASE $(MSSQL_DATABASE_NAME);
GO

print "Crafting Database --------> $(MSSQL_DATABASE_NAME) ..."

USE $(MSSQL_DATABASE_NAME)
GO

CREATE LOGIN $(MSSQL_USER)
    WITH PASSWORD = '$(MSSQL_USER_PASSWORD)';
GO

print "Creating Login to User ---------> $(MSSQL_USER) ..."

CREATE USER $(MSSQL_USER) FOR LOGIN $(MSSQL_USER) WITH DEFAULT_SCHEMA=[dbo]
GO

print "Creating User --------> $(MSSQL_USER) ..."

GRANT CONTROL TO $(MSSQL_USER);
GO

print "Grant all privileges to User $(MSSQL_USER) in $(MSSQL_DATABASE_NAME)..."

-- CREATE TABLE Users (
--     id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--     email VARCHAR(50) NOT NULL UNIQUE,
--     created_at DATETIME DEFAULT CURRENT_TIMESTAMP
-- );

-- print "Creating table users and Seeding data..."

-- INSERT INTO Users (email)VALUES("fbynuimo");

