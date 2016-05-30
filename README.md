# AddressBook

# Pre-Requsite Steps:
# 1- Setup Database:
# Contact Table
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

# User Table