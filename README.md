# AddressBook

 Pre-Requsite Steps:

 1- Setup Database:
 Create Database AddressBook

  2- Create Contact Table
CREATE TABLE [dbo].[Contact]
(
	[Id] INT NOT NULL IDENTITY(1,1) ,
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50)  NULL,
	[PhoneNumber] NVARCHAR(10) NOT NULL,
	[StreetName] NVARCHAR(30) NOT NULL,
	[City] NVARCHAR(30) NOT NULL,
	[Province] NVARCHAR(30) NOT NULL,
	[PostalCode] NVARCHAR(10) NOT Null,
	[Country] NVARCHAR(30) NOT NULL,
	PRIMARY KEY ([Id])
)

GO

CREATE INDEX [IX_Contact_Id] ON [dbo].[Contact] ([Id])
GO

INSERT INTO [dbo].[Contact]
	([FirstName], [LastName], [PhoneNumber], [StreetName], [City], [Province], [PostalCode], [Country])
VALUES
	('Monica','Parse', '6472013717', 'C Street', 'Toronto', 'Ontario', 'M552H5', 'Canada')
GO

3-User Table
CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY(1,1) ,
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50)  NULL,
	[PasswordHashed] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	IsAdmin bit default 0,
	resetPassword nvarchar(50)
	PRIMARY KEY ([Id])
)
GO
CREATE INDEX [IX_Users_Username] ON [dbo].[Users] ([FirstName])
GO
INSERT INTO [dbo].[Users]
	([FirstName],[LastName], [PasswordHashed], [Email], IsAdmin)
VALUES
	('Admin', 'a','abc', 'admin@admin.com', 1)
GO