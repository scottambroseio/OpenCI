/****** Script for SelectTopNRows command from SSMS  ******/
use [OpenCI.Identity]
GO

CREATE PROC [Identity].[spGetAllRoles]
AS
SELECT *
FROM [Identity].[Role]
GO

CREATE PROC [Identity].[spGetRoleByName]
    @Name nvarchar
AS
SELECT *
FROM [Identity].[Role]
WHERE [Role].[Name] = @Name
GO

CREATE PROC [Identity].[spGetRoleById]
    @Id int
AS
SELECT *
FROM [Identity].[Role]
WHERE [Role].Id = @Id
GO

CREATE PROC [Identity].[spUpdateRole]
    @Id int,
    @Name nvarchar
AS
UPDATE [Identity].[Role] SET [Name] = @Name WHERE [Id] = @Id;
GO

CREATE PROC [Identity].[spDeleteRole]
    @Name nvarchar
AS
DELETE FROM [Identity].[Role] WHERE [Name] = @Name;
GO

CREATE PROC [Identity].[spCreateRole]
    @Name nvarchar
AS
INSERT INTO [Identity].[Role]
VALUES
    (@Name)
GO