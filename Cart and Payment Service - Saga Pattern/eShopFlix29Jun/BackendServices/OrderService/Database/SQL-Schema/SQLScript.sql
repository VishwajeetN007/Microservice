USE [OrderService29Jun]


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Orders](
	[OrderId] [uniqueidentifier] NOT NULL,
	[Product] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NULL,
	[UserId] [nvarchar](max) NULL,
	[OrderAcceptDateTime] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderState](
	[CorrelationId] [uniqueidentifier] NOT NULL,
	[CurrentState] [nvarchar](max) NULL,
	[OrderCreationDateTime] [datetime] NULL,
	[OrderCancelDateTime] [datetime] NULL,
	[OrderAcceptDateTime] [datetime] NULL,
	[OrderId] [uniqueidentifier] NULL,
	[PaymentId] [nvarchar](max) NULL, -- Newly added
	--[Price] [decimal](18, 2) NULL,  -- Commented
	[Products] [nvarchar](max) NULL,  -- Change Column name from Product to Products.
	CartId [int] NULL
 CONSTRAINT [PK_OrderStateData] PRIMARY KEY CLUSTERED 
(
	[CorrelationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO