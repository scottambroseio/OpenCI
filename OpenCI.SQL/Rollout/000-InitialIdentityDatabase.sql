USE [OpenCI.Identity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE SCHEMA [Identity]
GO

CREATE TABLE [Identity].[User]
(
    [Id] INT IDENTITY NOT NULL,
    [UserName] NVARCHAR (MAX) NOT NULL,
    [SecurityStamp] NVARCHAR (MAX) NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NULL,
    [EmailConfirmed] BIT NOT NULL DEFAULT 0,
    [Email] NVARCHAR (MAX) NOT NULL,
    [PhoneNumber] NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT NOT NULL DEFAULT 0,
    [AccessFailedCount] INT NOT NULL DEFAULT 0,
    [LockoutEnabled] BIT NOT NULL DEFAULT 0,
    [LockoutEndDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [TwoFactorEnabled] BIT NOT NULL DEFAULT 0,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [Identity].[Role]
(
    [Id] INT IDENTITY NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [Identity].[Claim]
(
    [Id] INT IDENTITY NOT NULL,
    [UserId] INT NOT NULL,
    [Type] NVARCHAR (MAX) NOT NULL,
    [Value] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id])
);
GO

CREATE TABLE [Identity].[UserLogin]
(
    [Id] INT IDENTITY NOT NULL,
    [LoginProvider] NVARCHAR (MAX) NOT NULL,
    [ProviderKey] NVARCHAR (MAX) NOT NULL,
    [UserId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id])
);
GO

CREATE TABLE [Identity].[UserRole]
(
    [Id] INT IDENTITY NOT NULL,
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]),
    FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Role] ([Id])
);
GO