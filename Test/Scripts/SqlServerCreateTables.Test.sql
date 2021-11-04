/* 
 * SQL Server Script
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *     Configure the @Default_DB_Path variable with the path where 
 *     database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */


 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\BasesPruebas\'
 
USE [master]

/***** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'practicamad_test')
DROP DATABASE [practicamad_test]

USE [master]


/* DataBase Creation */

	                              
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [practicamad_test] 
    ON PRIMARY ( NAME = practicamad_test, FILENAME = "' + @Default_DB_Path + N'practicamad_test.mdf")
    LOG ON ( NAME = practicamad_test_log, FILENAME = "' + @Default_DB_Path + N'practicamad_test_log.ldf")'

EXEC(@sql)
PRINT N'Database [PracticaMaD_test] created.'
GO

USE [practicamad_test]

/* ********** Drop Table UserProfile if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Likes]') AND type in ('U'))
DROP TABLE [Likes]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Photo]') AND type in ('U'))
DROP TABLE [Photo]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Tag]') AND type in ('U'))
DROP TABLE [Tag]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Follow]') AND type in ('U'))
DROP TABLE [Follow]
GO



/*
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */

CREATE TABLE UserProfile (
    userId bigint IDENTITY(1,1) NOT NULL,
    loginName varchar(100) NOT NULL,
    userPassword varchar(100) NOT NULL,
    firstName varchar(100) NOT NULL,
    lastName varchar(100) NOT NULL,
    email varchar(100) NOT NULL,
    internalization varchar(20) NOT NULL,

    CONSTRAINT [PK_UserProfile] PRIMARY KEY (userId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
);

CREATE TABLE Category (
    categoryId bigint IDENTITY(1,1) NOT NULL,
    categoryType varchar(100) NOT NULL CHECK (categoryType IN ('typeTest','type2')),

    CONSTRAINT [PK_Category] PRIMARY KEY (categoryId),
	CONSTRAINT [UniqueKey_CategorType] UNIQUE (categoryType)
);

CREATE TABLE Photo (
    photoId bigint IDENTITY(1,1) NOT NULL,
    title varchar(100) NOT NULL,
    photoDescription varchar(100) NOT NULL,
    photoDate DATETIME NOT NULL,
    f bigint NOT NULL,
    t bigint NOT NULL,
    iso varchar(100) NOT NULL,
    wb bigint NOT NULL,
    categoryId bigint NOT NULL,
    userId bigint NOT NULL,

    CONSTRAINT [PK_Photo] PRIMARY KEY (photoId),
    CONSTRAINT [FK_CategoryPhoto] FOREIGN KEY (categoryId)
        REFERENCES Category (categoryId),
    CONSTRAINT [FK_UserPhoto] FOREIGN KEY (userId)
        REFERENCES UserProfile (userId)
);

CREATE TABLE Tag (
    tagId bigint IDENTITY(1,1) NOT NULL,
    tagName varchar(100) NOT NULL,
    userId bigint NOT NULL,
    photoId bigint NOT NULL,

    CONSTRAINT [PK_Tag] PRIMARY KEY (tagId),
	CONSTRAINT [UniqueKey_Name] UNIQUE (tagName),
    CONSTRAINT [FK_UserTag] FOREIGN KEY (userId)
        REFERENCES UserProfile (userId),
    CONSTRAINT [FK_PhotoTag] FOREIGN KEY (photoId)
        REFERENCES Photo(photoId)
);


CREATE TABLE [Follow] (
    userId1 bigint NOT NULL,
    userId2 bigint NOT NULL,

	CONSTRAINT [PK_Follow] PRIMARY KEY (userId1, userId2),
    CONSTRAINT [FK_User1Id]  FOREIGN KEY (userId1)
        REFERENCES UserProfile (userId),
    CONSTRAINT [FK_User2Id]  FOREIGN KEY (userId2)
        REFERENCES UserProfile (userId) 
);

CREATE TABLE [Likes] (
    userId bigint NOT NULL,
    photoId bigint NOT NULL,

	CONSTRAINT [PK_Likes] PRIMARY KEY (userId, photoId),
    CONSTRAINT [FK_UserLikes]  FOREIGN KEY (userId)
        REFERENCES UserProfile(userId),
    CONSTRAINT [FK_PhotoLikes]  FOREIGN KEY (photoId)
        REFERENCES Photo(photoId) 
);

CREATE TABLE Comment(
    commentId bigint IDENTITY(1,1) NOT NULL,
    commentDescription varchar(100) NOT NULL,
    commentDate DATETIME NOT NULL,
    userId bigint NOT NULL,
    photoId bigint NOT NULL,

    CONSTRAINT [PK_Comment] PRIMARY KEY (commentId),
    CONSTRAINT [FK_UserComment] FOREIGN KEY (userId)
        REFERENCES UserProfile(userId),
    CONSTRAINT [FK_PhotoComment] FOREIGN KEY (photoId)
        REFERENCES Photo(photoId)
);


CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Tables UserProfile, Tag, Category, Comment and Photo created.'
GO

GO
