-- Check if the database exists
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'fintrellis_mro')
BEGIN
    -- If the database does not exist, create it
    DECLARE @sql_db NVARCHAR(MAX);
    SET @sql_db = 'CREATE DATABASE [fintrellis_mro]';
    EXEC sp_executesql @sql_db;
    PRINT 'Database created successfully.';
END
ELSE
BEGIN
    PRINT 'Database already exists.';
END
GO

USE fintrellis_mro
GO

-- Check if the table exists in the specified schema
IF NOT EXISTS (SELECT * 
               FROM INFORMATION_SCHEMA.TABLES 
               WHERE TABLE_SCHEMA = 'dbo'
               AND TABLE_NAME = 'Users')
BEGIN
    -- If the table does not exist, create it
    DECLARE @sql_user_tbl NVARCHAR(MAX);
    SET @sql_user_tbl = 'CREATE TABLE [Users] (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(64) NOT NULL,
        Email NVARCHAR(255) NOT NULL
    )';
    EXEC sp_executesql @sql_user_tbl;
    PRINT 'Table created successfully.';
END
ELSE
BEGIN
    PRINT 'Table already exists.';
END
GO

IF NOT EXISTS (SELECT * 
               FROM INFORMATION_SCHEMA.TABLES 
               WHERE TABLE_SCHEMA = 'dbo'
               AND TABLE_NAME = 'Blogs')
BEGIN
    -- If the table does not exist, create it
    DECLARE @sql_blog_tbl NVARCHAR(MAX);
    SET @sql_blog_tbl = 'CREATE TABLE [Blogs] (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(64) NOT NULL,
        Content NVARCHAR(MAX),
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UserId INT,
        FOREIGN KEY (UserId) REFERENCES Users(Id)
    )';
    EXEC sp_executesql @sql_blog_tbl;
    PRINT 'Table created successfully.';
END
ELSE
BEGIN
    PRINT 'Table already exists.';
END
GO