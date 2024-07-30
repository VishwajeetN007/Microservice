USE [StockService29Jun]
GO
/****** Object:  Table [dbo].[Stocks]    Script Date: 07-05-2024 17:12:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stocks](
	[StockId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_Stocks] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/******* Data *********/

GO
SET IDENTITY_INSERT [dbo].[Stocks] ON 
GO
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity], [LastUpdated]) VALUES (1, 1, 98, CAST(N'2023-12-12T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity], [LastUpdated]) VALUES (2, 2, 95, CAST(N'2024-12-12T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity], [LastUpdated]) VALUES (3, 3, 100, CAST(N'2023-12-12T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity], [LastUpdated]) VALUES (4, 4, 100, CAST(N'2023-12-12T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Stocks] OFF
GO