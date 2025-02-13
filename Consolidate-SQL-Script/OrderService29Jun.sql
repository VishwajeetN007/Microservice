USE [OrderService29Jun]
GO
/****** Object:  Table [dbo].[OrderState]    Script Date: 03-08-2024 22:16:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderState]') AND type in (N'U'))
DROP TABLE [dbo].[OrderState]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 03-08-2024 22:16:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U'))
DROP TABLE [dbo].[Orders]
GO
USE [master]
GO
/****** Object:  Database [OrderService29Jun]    Script Date: 03-08-2024 22:16:56 ******/
DROP DATABASE [OrderService29Jun]
GO
/****** Object:  Database [OrderService29Jun]    Script Date: 03-08-2024 22:16:56 ******/
CREATE DATABASE [OrderService29Jun]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OrderService29Jun', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OrderService29Jun.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OrderService29Jun_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OrderService29Jun_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OrderService29Jun] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OrderService29Jun].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OrderService29Jun] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OrderService29Jun] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OrderService29Jun] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OrderService29Jun] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OrderService29Jun] SET ARITHABORT OFF 
GO
ALTER DATABASE [OrderService29Jun] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OrderService29Jun] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OrderService29Jun] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OrderService29Jun] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OrderService29Jun] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OrderService29Jun] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OrderService29Jun] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OrderService29Jun] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OrderService29Jun] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OrderService29Jun] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OrderService29Jun] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OrderService29Jun] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OrderService29Jun] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OrderService29Jun] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OrderService29Jun] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OrderService29Jun] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OrderService29Jun] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OrderService29Jun] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OrderService29Jun] SET  MULTI_USER 
GO
ALTER DATABASE [OrderService29Jun] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OrderService29Jun] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OrderService29Jun] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OrderService29Jun] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OrderService29Jun] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OrderService29Jun] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OrderService29Jun] SET QUERY_STORE = OFF
GO
USE [OrderService29Jun]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 03-08-2024 22:16:56 ******/
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
/****** Object:  Table [dbo].[OrderState]    Script Date: 03-08-2024 22:16:56 ******/
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
	[Price] [decimal](18, 2) NULL,
	[Product] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderStateData] PRIMARY KEY CLUSTERED 
(
	[CorrelationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [OrderService29Jun] SET  READ_WRITE 
GO
