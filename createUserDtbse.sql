USE master;
GO

IF DB_ID(N'SignalR_User') IS NULL
  CREATE DATABASE SignalR_User;
GO

USE SignalR_User;
GO 

IF OBJECT_ID('UserR') IS NOT NULL
  DROP TABLE UserR;
go

CREATE TABLE UserR (
  UserName nvarchar(50) primary key,
  Password nvarchar(50),
  LastKnownIP nvarchar(50),
  PreName nvarchar(50),
  LastName nvarchar(50)
);

insert into UserR(UserName,Password)
values ('Admin','123456'),('Daniel','123456'),('Kirk','123456'),('Yoda','123456')