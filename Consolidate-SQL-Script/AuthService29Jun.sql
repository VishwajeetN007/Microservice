USE [AuthService29Jun]
GO
ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03-08-2024 21:39:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 03-08-2024 21:39:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[UserRoles]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03-08-2024 21:39:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
DROP TABLE [dbo].[Roles]
GO
USE [master]
GO
/****** Object:  Database [AuthService29Jun]    Script Date: 03-08-2024 21:39:18 ******/
DROP DATABASE [AuthService29Jun]
GO
/****** Object:  Database [AuthService29Jun]    Script Date: 03-08-2024 21:39:18 ******/
CREATE DATABASE [AuthService29Jun]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AuthService29Jun', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AuthService29Jun.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AuthService29Jun_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AuthService29Jun_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AuthService29Jun] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AuthService29Jun].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AuthService29Jun] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AuthService29Jun] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AuthService29Jun] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AuthService29Jun] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AuthService29Jun] SET ARITHABORT OFF 
GO
ALTER DATABASE [AuthService29Jun] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AuthService29Jun] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AuthService29Jun] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AuthService29Jun] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AuthService29Jun] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AuthService29Jun] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AuthService29Jun] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AuthService29Jun] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AuthService29Jun] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AuthService29Jun] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AuthService29Jun] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AuthService29Jun] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AuthService29Jun] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AuthService29Jun] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AuthService29Jun] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AuthService29Jun] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AuthService29Jun] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AuthService29Jun] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AuthService29Jun] SET  MULTI_USER 
GO
ALTER DATABASE [AuthService29Jun] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AuthService29Jun] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AuthService29Jun] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AuthService29Jun] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AuthService29Jun] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AuthService29Jun] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AuthService29Jun] SET QUERY_STORE = OFF
GO
USE [AuthService29Jun]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03-08-2024 21:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 03-08-2024 21:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03-08-2024 21:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 2)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (2, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [PhoneNumber], [EmailConfirmed], [CreatedDate]) VALUES (1, N'User', N'user@gmail.com', N'$2a$11$iTKXRsW8HvJ39mRXP14d.urshQj9nvQthfxI1xZhUuQq4UJIysQW6', N'9876543210', 0, CAST(N'2022-06-05T17:15:47.1917701' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [PhoneNumber], [EmailConfirmed], [CreatedDate]) VALUES (2, N'Admin', N'admin@gmail.com', N'$2a$11$i5l9/BdNnhwpIK8JaXkNROQZnTFJFc6eaDiSy.cQhyh8Vxec8olh.', N'9876543210', 0, CAST(N'2022-06-05T17:16:59.8050624' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [AuthService29Jun] SET  READ_WRITE 
GO
