﻿-- this script is used to create the database needed for tests
USE [master]
GO
if exists (select * from sysdatabases where name='modelTest')
ALTER DATABASE modelTest SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
if exists (select * from sysdatabases where name='modelTest')
DROP DATABASE modelTest
go
USE [master]
GO
CREATE DATABASE modelTest
go
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [modelTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
use modelTest
go
print 'database modelTest created and changed to '
go
CREATE TABLE [Project](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] nvarchar(250) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY  
(
	[ProjectId] ASC
)
) 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EmployeeRank](
	[RankId] [int] IDENTITY(1,1) NOT NULL,
	[Rank] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeeRank] PRIMARY KEY  
(
	[RankId] ASC
)
) 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](50) NOT NULL,
	[EmployeeRankId] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY  
(
	[EmployeeId] ASC
)
) 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EmployeeProject](
	[EmployeeProjectId] [int] IDENTITY(1,1) NOT NULL,
	[EPEmployeeId] [int] NOT NULL,
	[EPProjectId] [int] NOT NULL,
	[AssignDate] [date] NULL,
	[EndDate] [date] NULL,
	[Rate] [decimal](10, 2) NULL,
 CONSTRAINT [PK_EmployeeProject] PRIMARY KEY  
(
	[EmployeeProjectId] ASC
)
) 
GO
ALTER TABLE [Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EmployeeRank]
 FOREIGN KEY([EmployeeRankId])
REFERENCES [EmployeeRank] ([RankId])
GO
ALTER TABLE [Employee] CHECK CONSTRAINT [FK_Employee_EmployeeRank]
GO
ALTER TABLE [EmployeeProject]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProject_Employee] FOREIGN KEY([EPEmployeeId])
REFERENCES [Employee] ([EmployeeId])
GO
ALTER TABLE [EmployeeProject] CHECK CONSTRAINT [FK_EmployeeProject_Employee]
GO
ALTER TABLE [EmployeeProject]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProject_Project] FOREIGN KEY([EPProjectId])
REFERENCES [Project] ([ProjectId])
GO
ALTER TABLE [EmployeeProject] CHECK CONSTRAINT [FK_EmployeeProject_Project]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_EmployeeInfo_Employee]') AND parent_object_id = OBJECT_ID(N'[EmployeeInfo]'))
ALTER TABLE [EmployeeInfo] DROP CONSTRAINT [FK_EmployeeInfo_Employee]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeInfo]') AND type in (N'U'))
DROP TABLE [EmployeeInfo]
GO
CREATE TABLE [EmployeeInfo](
	[EmployeeInfoId] [int] IDENTITY NOT NULL,
	[EIEmployeeId] [int] NOT NULL,
	[Salary] [decimal](10, 2) NULL,
	[Address] [nvarchar](600) NULL,
 CONSTRAINT [PK_EmployeeInfo] PRIMARY KEY ([EmployeeInfoId] ASC))

GO
ALTER TABLE [EmployeeInfo]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeInfo_Employee] FOREIGN KEY([EIEmployeeId])
REFERENCES [Employee] ([EmployeeId])
GO
ALTER TABLE [EmployeeInfo] CHECK CONSTRAINT [FK_EmployeeInfo_Employee]
GO
set IDENTITY_INSERT dbo.EmployeeRank ON
go
insert into EmployeeRank (rankId, Rank) values (1,'President')
insert into EmployeeRank (rankId, Rank) values (2,'Associate Consultant')
insert into EmployeeRank (rankId, Rank) values (3,'Snior Consultant')
go
set IDENTITY_INSERT dbo.EmployeeRank OFF
go
set IDENTITY_INSERT dbo.Project ON
go
insert into Project (ProjectId, ProjectName) values (1,'Cyprus Police')
insert into Project (ProjectId, ProjectName) values (2,'Cyprus VAT')
insert into Project (ProjectId, ProjectName) values (3,'EDY')
insert into Project (ProjectId, ProjectName) values (4,'MARLOW')
go
set IDENTITY_INSERT dbo.Project OFF
go
set IDENTITY_INSERT dbo.Employee ON
go
insert into Employee (Employeeid, Employeename,EmployeeRankId) values (1,'Barack Obama',1)
insert into Employee (Employeeid, Employeename,EmployeeRankId) values (2,'George Bush',2)
insert into Employee (Employeeid, Employeename,EmployeeRankId) values (3,'George Clunie',3)
insert into Employee (Employeeid, Employeename,EmployeeRankId) values (4,'Melissa Domme',2)
go
set IDENTITY_INSERT dbo.Employee OFF
go
insert into EmployeeInfo (EIEmployeeid, Salary) values (1,3000)
insert into EmployeeInfo (EIEmployeeid, Salary) values (2,1000)
insert into EmployeeInfo (EIEmployeeid, Salary) values (3,3500)
insert into EmployeeInfo (EIEmployeeid, Salary) values (4,1500)
go
insert into [EmployeeProject] ([EPEmployeeId],[EPProjectId],[AssignDate],[EndDate],[Rate])
values (1,1, dateAdd(m,-34, getDate()),dateAdd(m,-3, getDate()),500.00)
insert into [EmployeeProject] ([EPEmployeeId],[EPProjectId],[AssignDate],[EndDate],[Rate])
values (1,2,dateAdd(m,-13, getDate()),null,1000.00)
insert into [EmployeeProject] ([EPEmployeeId],[EPProjectId],[AssignDate],[EndDate],[Rate])
values (1,3,dateAdd(m,-1, getDate()),null,700.00)
insert into [EmployeeProject] ([EPEmployeeId],[EPProjectId],[AssignDate],[EndDate],[Rate])
values (2,1, dateAdd(m,-12, getDate()),null,100.00)
insert into [EmployeeProject] ([EPEmployeeId],[EPProjectId],[AssignDate],[EndDate],[Rate])
values (2,2,dateAdd(m,-10, getDate()),null,100.00)
insert into [EmployeeProject] ([EPEmployeeId],[EPProjectId],[AssignDate],[EndDate],[Rate])
values (2,3,dateAdd(m,-3, getDate()),null,100.00)
go
ALTER TABLE dbo.Employee ADD
	Salary decimal(10,2) null, [Address] nvarchar(50) NULL,
	Telephone nvarchar(10) NULL,
	Mobile nvarchar(10) NULL,
	IdNumber nvarchar(10) NULL,
	SSINumber nvarchar(10) NULL,
	HireDate date NULL
GO
ALTER TABLE dbo.Employee ADD
	NumDependents int null
go
ALTER TABLE dbo.Project ADD
	isActive int not null default 1
go
CREATE TABLE [dbo].sysLanguageStrings(
[langKey] [varchar](70) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[langValueEN] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[langValueEL] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
CONSTRAINT [PK_sysLanguageStrings] PRIMARY KEY ([langKey])) 