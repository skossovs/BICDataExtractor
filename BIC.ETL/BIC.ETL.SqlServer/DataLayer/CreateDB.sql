﻿-- * * * * * * * *     C R E A T E   T A B L E S    * * * * * * * *
USE [BIC]
GO

/****** Object:  Table [dbo].[Sector]    Script Date: 3/19/2021 12:00:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sector](
	[SectorID] [int] IDENTITY(1,1) NOT NULL,
	[Sector] [nvarchar](200) NOT NULL
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
ADD CONSTRAINT PK_Industry PRIMARY KEY (SectorID, Industry)

ALTER TABLE dbo.[Sector]
ADD CONSTRAINT PK_Sector PRIMARY KEY (Sector)

ALTER TABLE dbo.[Security]
ADD CONSTRAINT PK_Security PRIMARY KEY (SecurityID)

ALTER TABLE dbo.[TimeDimmension]
ADD CONSTRAINT PK_TimeDimmension PRIMARY KEY (TimeID)

GO

CREATE TABLE [dbo].[CashFlowQuarterly](
	[SecurityID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Quarter] [int] NOT NULL,
	[endDate] [date] NOT NULL,
	[investments] [numeric](38, 0) NULL,
	[netBorrowings] [numeric](38, 0) NULL,
	[netIncome] [numeric](38, 0) NULL,
	[issuanceOfStock] [numeric](38, 0) NULL,
	[repurchaseOfStock] [numeric](38, 0) NULL,
	[effectOfExchangeRate] [numeric](38, 0) NULL,
	[depreciation] [numeric](38, 0) NULL,
	[dividendsPaid] [numeric](38, 0) NULL,
	[changeInCash] [numeric](38, 0) NULL,
	[changeToLiabilities] [numeric](38, 0) NULL,
	[changeToOperatingActivities] [numeric](38, 0) NULL,
	[changeToInventory] [numeric](38, 0) NULL,
	[changeToAccountReceivables] [numeric](38, 0) NULL,
	[otherCashflowsFromInvestingActivities] [numeric](38, 0) NULL,
	[otherCashflowsFromFinancingActivities] [numeric](38, 0) NULL,
	[totalCashflowsFromInvestingActivities] [numeric](38, 0) NULL,
	[totalCashFromFinancingActivities] [numeric](38, 0) NULL,
	[totalCashFromOperatingActivities] [numeric](38, 0) NULL,
	[capitalExpenditures] [numeric](38, 0) NULL,
 CONSTRAINT [PK_CashFlowQuarterly] PRIMARY KEY CLUSTERED 
(
	[SecurityID] ASC,
	[Year] ASC,
	[Quarter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BalanceSheetQuarterly](
	[SecurityID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Quarter] [int] NOT NULL,
	[endDate] [date] NOT NULL,
	[cash] [decimal](38, 0) NULL,
	[intangibleAssets] [decimal](38, 0) NULL,
	[otherCurrentAssets] [decimal](38, 0) NULL,
	[totalCurrentAssets] [decimal](38, 0) NULL,
	[goodWill] [decimal](38, 0) NULL,
	[retainedEarnings] [decimal](38, 0) NULL,
	[propertyPlantEquipment] [decimal](38, 0) NULL,
	[longTermInvestments] [decimal](38, 0) NULL,
	[shortTermInvestments] [decimal](38, 0) NULL,
	[netReceivables] [decimal](38, 0) NULL,
	[inventory] [decimal](38, 0) NULL,
	[accountsPayable] [decimal](38, 0) NULL,
	[otherAssets] [decimal](38, 0) NULL,
	[totalAssets] [decimal](38, 0) NULL,
	[otherCurrentLiab] [decimal](38, 0) NULL,
	[totalCurrentLiabilities] [decimal](38, 0) NULL,
	[shortLongTermDebt] [decimal](38, 0) NULL,
	[otherLiab] [decimal](38, 0) NULL,
	[longTermDebt] [decimal](38, 0) NULL,
	[totalLiab] [decimal](38, 0) NULL,
	[netTangibleAssets] [decimal](38, 0) NULL,
	[totalStockholderEquity] [decimal](38, 0) NULL,
	[commonStock] [decimal](38, 0) NULL,
	[otherStockholderEquity] [decimal](38, 0) NULL,
	[treasuryStock] [decimal](38, 0) NULL,
 CONSTRAINT [PK_BalanceSheetQuarterly] PRIMARY KEY CLUSTERED 
(
	[SecurityID] ASC,
	[Year] ASC,
	[Quarter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[IncomeStatementQuarterly](
	[SecurityID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Quarter] [int] NOT NULL,
	[endDate] [date] NOT NULL,
	[totalRevenue] [numeric](38, 0) NULL,
	[costOfRevenue] [numeric](38, 0) NULL,
	[researchDevelopment] [numeric](38, 0) NULL,
	[totalOperatingExpenses] [numeric](38, 0) NULL,
	[totalOtherIncomeExpenseNet] [numeric](38, 0) NULL,
	[otherOperatingExpenses] [numeric](38, 0) NULL,
	[minorityInterest] [numeric](38, 0) NULL,
	[interestExpense] [numeric](38, 0) NULL,
	[extraordinaryItems] [numeric](38, 0) NULL,
	[sellingGeneralAdministrative] [numeric](38, 0) NULL,
	[nonRecurring] [numeric](38, 0) NULL,
	[otherItems] [numeric](38, 0) NULL,
	[incomeTaxExpense] [numeric](38, 0) NULL,
	[netIncomeFromContinuingOps] [numeric](38, 0) NULL,
	[netIncomeApplicableToCommonShares] [numeric](38, 0) NULL,
	[discontinuedOperations] [numeric](38, 0) NULL,
	[effectOfAccountingCharges] [numeric](38, 0) NULL,
	[incomeBeforeTax] [numeric](38, 0) NULL,
	[ebit] [numeric](38, 0) NULL,
	[operatingIncome] [numeric](38, 0) NULL,
	[netIncome] [numeric](38, 0) NULL,
	[grossProfit] [numeric](38, 0) NULL,
 CONSTRAINT [PK_IncomeStatementQuarterly] PRIMARY KEY CLUSTERED 
(
	[SecurityID] ASC,
	[Year] ASC,
	[Quarter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[FxUsdRates](
	[FxRateId] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Quarter] [int] NOT NULL,
	[Currency] [nvarchar](3) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
 CONSTRAINT [PK_FxUsdRates] PRIMARY KEY CLUSTERED 
(
	[Year] ASC,
	[Quarter] ASC,
	[Currency] ASC,
	[Country] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[CurrencyCountryMap](
	[Currency] [nvarchar](5) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CurrencyCountryMap] PRIMARY KEY CLUSTERED 
(
	[Currency] ASC,
	[Country] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



-- * * * * * * * *          R E P O R T S      * * * * * * * *
-- 1. join tables
SELECT
	s.SecurityID
,	s.Ticker
,	b.[Year]
,   b.[Quarter]
,   b.[totalStockholderEquity] as Equity
FROM		Security s
LEFT  JOIN  BalanceSheetQuarterly b
ON			b.SecurityID = s.SecurityID
LEFT  JOIN  IncomeStatementQuarterly i
ON          i.SecurityID = s.SecurityID
        AND i.[Year]     = b.[Year]
		AND i.[Quarter]  = b.[Quarter]
LEFT  JOIN  CashFlowQuarterly c
ON          c.SecurityID = s.SecurityID
        AND c.[Year]     = b.[Year]
		AND c.[Quarter]  = b.[Quarter]
LEFT   JOIN [dbo].[KeyRatio]  k
ON          k.SecurityID = s.SecurityID
        AND k.[Year]     = b.[Year]
		AND k.[Quarter]  = b.[Quarter]
GO

-- 1.1. Statistics
SELECT [SectorID] ,[Sector]
  FROM [BIC].[dbo].[Sector]

SELECT s.Sector, i.Industry, Count(*) c FROM [Security] t
INNER JOIN    Sector     s ON t.SectorID = s.SectorID
INNER JOIN    Industry   i ON i.IndustryID = t.IndustryID
LEFT  JOIN    BalanceSheetQuarterly b ON b.SecurityID = t.SecurityID
WHERE b.Year = 2020 and b.Quarter = 4 AND  b.SecurityID is not null
GROUP BY s.Sector, i.Industry
ORDER BY s.Sector, i.Industry

SELECT * FROM LevelZeroScreener
WHERE Quarter = 4
ORDER BY Sector, Industry, Ticker

GO
-- 2. Strategy "Value Stocks"


ALTER VIEW [dbo].[LevelZeroScreener]
AS
SELECT        
	s.SecurityID
,	s.Ticker
, sc.Sector
, ids.Industry
, b.Year
, b.Quarter
, fx.Rate * (b.totalStockholderEquity - COALESCE (b.goodWill, 0) + COALESCE (b.retainedEarnings, 0)) / k.MarketCap AS Worthiness
, COALESCE(b.intangibleAssets, b.goodWill) / NULLIF(netTangibleAssets,0) AS intagnibleRatio
, fx.Rate * b.totalStockholderEquity AS Equity
, fx.Rate * b.retainedEarnings AS RetainedEarnings
, k.MarketCap
, COALESCE(k.CurrentRatio, totalCurrentAssets/NULLIF(totalCurrentLiabilities,0)) AS CurrentRatio
, k.QuickRatio
, k.LongTermDebtToEquity
, b.longTermDebt / NULLIF((b.[totalAssets] - b.[totalCurrentAssets]),0) AS DebtToAssets
, k.DebtToEquity
, k.GrossMargin
, k.OperationMargin
, k.ProfitMargin
, k.Volume
, ((b.commonStock * 1000)/NULLIF(k.Volume,0)) AS Liquidity
FROM    dbo.Security AS s INNER JOIN
		dbo.Sector AS sc ON s.SectorID = sc.SectorID INNER JOIN
		dbo.Industry AS ids ON s.IndustryID = ids.IndustryID LEFT OUTER JOIN
		dbo.BalanceSheetQuarterly AS b ON b.SecurityID = s.SecurityID LEFT OUTER JOIN
		dbo.IncomeStatementQuarterly AS i ON i.SecurityID = s.SecurityID AND i.Year = b.Year AND i.Quarter = b.Quarter LEFT OUTER JOIN
		dbo.CashFlowQuarterly AS c ON c.SecurityID = s.SecurityID AND c.Year = b.Year AND c.Quarter = b.Quarter LEFT OUTER JOIN
		dbo.KeyRatio AS k ON k.SecurityID = s.SecurityID AND k.Year = b.Year AND k.Quarter = b.Quarter LEFT OUTER JOIN
		dbo.FxUsdRates fx ON fx.Country = s.Country AND fx.[Year] = b.Year AND fx.Quarter = b.Quarter

GO

-- 3. Strategy "Growth Stocks"
-- 4. Strategy "Penny Stocks"


-- * * * * * * * *     I N S E R T   D A T A   * * * * * * * *
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
