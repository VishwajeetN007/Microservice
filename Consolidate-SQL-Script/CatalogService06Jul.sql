USE [CatalogService06Jul]
GO
ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_Categories_CategoryId]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 03-08-2024 22:15:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
DROP TABLE [dbo].[Products]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 03-08-2024 22:15:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
DROP TABLE [dbo].[Categories]
GO
USE [master]
GO
/****** Object:  Database [CatalogService06Jul]    Script Date: 03-08-2024 22:15:51 ******/
DROP DATABASE [CatalogService06Jul]
GO
/****** Object:  Database [CatalogService06Jul]    Script Date: 03-08-2024 22:15:51 ******/
CREATE DATABASE [CatalogService06Jul]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CatalogService06Jul', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CatalogService06Jul.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CatalogService06Jul_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CatalogService06Jul_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CatalogService06Jul] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CatalogService06Jul].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CatalogService06Jul] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET ARITHABORT OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CatalogService06Jul] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CatalogService06Jul] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CatalogService06Jul] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CatalogService06Jul] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CatalogService06Jul] SET  MULTI_USER 
GO
ALTER DATABASE [CatalogService06Jul] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CatalogService06Jul] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CatalogService06Jul] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CatalogService06Jul] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CatalogService06Jul] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CatalogService06Jul] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CatalogService06Jul] SET QUERY_STORE = OFF
GO
USE [CatalogService06Jul]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 03-08-2024 22:15:51 ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 03-08-2024 22:15:51 ******/
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
USE [master]
GO
ALTER DATABASE [CatalogService06Jul] SET  READ_WRITE 
GO
