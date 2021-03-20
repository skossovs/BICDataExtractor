USE [BIC]
GO

/****** Object:  Table [dbo].[Sector]    Script Date: 3/19/2021 12:00:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sector](
	[SectorID] [int] IDENTITY(1,1) NOT NULL,
	[Sector] [nvarchar](200) NULL
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Industry]    Script Date: 3/19/2021 12:00:34 AM ******/
CREATE TABLE [dbo].[Industry](
	[IndustryID] [int] IDENTITY(1,1) NOT NULL,
	[SectorID] [int] NOT NULL,
	[Industry] [nvarchar](200) NOT NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Security](
	[SecurityID] [int] IDENTITY(1,1) NOT NULL,
	[SectorID]   [int] NOT NULL,
	[IndustryID] [int] NOT NULL,
	[Ticker] [nvarchar](6) NOT NULL,
	[Company] [nvarchar](200) NOT NULL,
	[Country] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO

