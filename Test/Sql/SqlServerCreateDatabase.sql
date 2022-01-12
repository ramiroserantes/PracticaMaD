USE [practicamadTest]

/* ********** Drop Table UserProfile if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Tag]') AND type in ('U'))
DROP TABLE [Tag]
GO

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


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Follow]') AND type in ('U'))
DROP TABLE [Follow]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO


CREATE TABLE UserProfile (
    userId bigint IDENTITY(1,1) NOT NULL,
    loginName varchar(100) NOT NULL,
    userPassword varchar(100) NOT NULL,
    firstName varchar(100) NOT NULL,
    lastName varchar(100) NOT NULL,
    email varchar(100) NOT NULL,
    lenguage varchar(20) NOT NULL,
    country varchar(2) NOT NULL,

    CONSTRAINT [PK_UserProfile] PRIMARY KEY (userId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
);

CREATE TABLE Category (
    categoryId bigint IDENTITY(1,1) NOT NULL,
    categoryType varchar(100) NOT NULL,

    CONSTRAINT [PK_Category] PRIMARY KEY (categoryId),
	CONSTRAINT [UniqueKey_CategorType] UNIQUE (categoryType)
);

CREATE TABLE Photo (
    photoId bigint IDENTITY(1,1) NOT NULL,
    userName varchar(100) NOT NULL,
    title varchar(100) NOT NULL,
    photoDescription varchar(100) NOT NULL,
    photoDate DATETIME NOT NULL,
    f bigint NOT NULL,
    t bigint NOT NULL,
    iso varchar(100) NOT NULL,
    wb bigint NOT NULL,
	link varchar(200) NULL,
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
    photoId bigint NULL,

    CONSTRAINT [PK_Tag] PRIMARY KEY (tagId),
	CONSTRAINT [UniqueKey_Name] UNIQUE (tagName),
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
    userName varchar(100) NOT NULL,
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
