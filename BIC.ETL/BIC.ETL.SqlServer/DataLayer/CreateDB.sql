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

CREATE TABLE [dbo].[TimeDimmension](
	[TimeID]  [int] NOT NULL,
	[Year]    [int] NOT NULL,
	[Quarter] [int] NOT NULL,
) ON [PRIMARY]

GO
-- Populate TimeDimmension
-- n-80 subject to change depends from which date you want to populate
With numbers AS
(SELECT TOP (3000) n = ROW_NUMBER() OVER (ORDER BY number) 
  FROM [master]..spt_values )
, dates AS
(SELECT DATEADD(DAY, n - 80, CAST(GETDATE() AS Date)) d FROM numbers)
, dims AS
(SELECT
	CONVERT(VARCHAR, d, 112)                                        d_int
,	((DATEPART(MONTH, d) - ((DATEPART(MONTH, d) - 1) % 3)) / 3 + 1) Q
,	DATEPART(YEAR, d)                                               Y
FROM dates)
SELECT * INTO #TimeDimmension FROM dims

INSERT INTO [TimeDimmension]
SELECT * FROM #TimeDimmension

-- Key Ratio table
CREATE TABLE [dbo].[KeyRatio](
	[SecurityID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Quarter] [int] NOT NULL,
	[MarketCap] [numeric](38, 6) NULL,
	[Dividend] [numeric](18, 6) NULL,
	[ROA] [numeric](18, 6) NULL,
	[ROE] [numeric](18, 6) NULL,
	[ROI] [numeric](18, 6) NULL,
	[CurrentRatio] [numeric](18, 6) NULL,
	[QuickRatio] [numeric](18, 6) NULL,
	[LongTermDebtToEquity] [numeric](18, 6) NULL,
	[DebtToEquity] [numeric](18, 6) NULL,
	[GrossMargin] [numeric](18, 6) NULL,
	[OperationMargin] [numeric](18, 6) NULL,
	[ProfitMargin] [numeric](18, 6) NULL,
	[Earnings] [numeric](18, 6) NULL,
	[Price] [numeric](18, 6) NULL,
	[Change] [numeric](18, 6) NULL,
	[Volume] [numeric](38, 6) NULL,
 CONSTRAINT [PK_KeyRatio] PRIMARY KEY CLUSTERED 
(
	[SecurityID] ASC,
	[Year] ASC,
	[Quarter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE dbo.[Industry]
ADD CONSTRAINT PK_Industry PRIMARY KEY (IndustryID)

ALTER TABLE dbo.[Sector]
ADD CONSTRAINT PK_Sector PRIMARY KEY (SectorID)

ALTER TABLE dbo.[Security]
ADD CONSTRAINT PK_Security PRIMARY KEY (SecurityID)

ALTER TABLE dbo.[TimeDimmension]
ADD CONSTRAINT PK_TimeDimmension PRIMARY KEY (TimeID)

GO