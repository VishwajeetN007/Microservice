USE [CatalogService06Jul]
GO
ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_Categories_CategoryId]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 23-07-2024 20:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
DROP TABLE [dbo].[Products]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 23-07-2024 20:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
DROP TABLE [dbo].[Categories]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 23-07-2024 20:42:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 23-07-2024 20:42:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (1, N'Phones')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (2, N'Laptops')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [CreatedDate]) VALUES (1, N'iPhone14', N'iPhone14', CAST(70000.00 AS Decimal(18, 2)), N'/images/iphone14.png', 1, CAST(N'2003-12-12T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [CreatedDate]) VALUES (2, N'Samsung S23', N'Samsung S23', CAST(50000.00 AS Decimal(18, 2)), N'/images/samsungs23.png', 1, CAST(N'2003-12-12T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [CreatedDate]) VALUES (3, N'Dell Laptop', N'Dell Laptop', CAST(60000.00 AS Decimal(18, 2)), N'/images/dell-laptop.png', 2, CAST(N'2003-12-12T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [CreatedDate]) VALUES (4, N'MacBook Pro', N'MacBook Pro', CAST(100000.00 AS Decimal(18, 2)), N'/images/macbook.png', 2, CAST(N'2003-12-12T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
