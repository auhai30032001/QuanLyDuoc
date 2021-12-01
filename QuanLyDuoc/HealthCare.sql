create database HealthCare
Go
use HealthCare
go

CREATE TABLE [dbo].[CustomerTbl] (
    [CustNum]   INT           IDENTITY (1, 1) NOT NULL,
    [CustName]  VARCHAR (50)  NOT NULL,
    [CustPhone] VARCHAR (50)  NOT NULL,
    [CustAdd]   VARCHAR (100) NOT NULL,
    [CustDOB]   DATE          NOT NULL,
    [CustGen]   VARCHAR (10)  NOT NULL,
    PRIMARY KEY CLUSTERED ([CustNum] ASC)
);
go

CREATE TABLE [dbo].[ManufacturerTbl] (
    [ManId]    INT           IDENTITY (500, 1) NOT NULL,
    [ManName]  VARCHAR (50)  NOT NULL,
    [ManAdd]   VARCHAR (100) NOT NULL,
    [ManPhone] VARCHAR (50)  NOT NULL,
    [ManJDate] DATE          NOT NULL,
    PRIMARY KEY CLUSTERED ([ManId] ASC)
);
go

CREATE TABLE [dbo].[MedicineTbl] (
    [MedNum]      INT          IDENTITY (100, 1) NOT NULL,
    [MedName]     VARCHAR (50) NOT NULL,
    [MedType]     VARCHAR (50) NOT NULL,
    [ MedQty]     INT          NOT NULL,
    [MedPrice]    INT          NOT NULL,
    [MedManId]    INT          NOT NULL,
    [MedManufact] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([MedNum] ASC),
    CONSTRAINT [FK2] FOREIGN KEY ([MedManId]) REFERENCES [dbo].[ManufacturerTbl] ([ManId])
);
go

CREATE TABLE [dbo].[SellerTbl] (
    [SNum]   INT           IDENTITY (1, 1) NOT NULL,
    [SName]  VARCHAR (50)  NOT NULL,
    [SDOB]   DATE          NOT NULL,
    [SPhone] VARCHAR (50)  NOT NULL,
    [SAdd]   VARCHAR (100) NOT NULL,
    [SGen]   VARCHAR (50)  NOT NULL,
    [SPass]  VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([SNum] ASC)
);
go

CREATE TABLE [dbo].[BillTbl] (
    [BNum]     INT          IDENTITY (1, 1) NOT NULL,
    [SName]    VARCHAR (50) NOT NULL,
    [CustNum]  INT          NOT NULL,
    [CustName] VARCHAR (50) NOT NULL,
    [BDate]    DATE         NOT NULL,
    [BAmount]  INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([BNum] ASC),
    CONSTRAINT [FK1] FOREIGN KEY ([CustNum]) REFERENCES [dbo].[CustomerTbl] ([CustNum])
);
go

sp_rename 'MedicineTbl. MedQty', 'MedQty','COLUMN';

ALTER TABLE MedicineTbl
  ALTER COLUMN MedName NVARCHAR(50) NOT NULL;

 ALTER TABLE MedicineTbl
  ALTER COLUMN MedType NVARCHAR(50) NOT NULL;

ALTER TABLE ManufacturerTbl
  ALTER COLUMN ManName NVARCHAR(50) NOT NULL;

  ALTER TABLE CustomerTbl
  ALTER COLUMN CustName NVARCHAR(50) NOT NULL;

  ALTER TABLE CustomerTbl
  ALTER COLUMN CustAdd NVARCHAR(50) NOT NULL;

  ALTER TABLE SellerTbl
  ALTER COLUMN SName NVARCHAR(50) NOT NULL;

   ALTER TABLE SellerTbl
  ALTER COLUMN SAdd NVARCHAR(50) NOT NULL;

   ALTER TABLE SellerTbl
  ALTER COLUMN SGen NVARCHAR(50) NOT NULL;

   ALTER TABLE SellerTbl
  ALTER COLUMN SPass VARCHAR(50) ;

