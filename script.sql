USE [master]
GO
/****** Object:  Database [ZEKA_IEP]    Script Date: 2/2/2019 5:49:15 PM ******/
CREATE DATABASE [ZEKA_IEP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ZEKA_IEP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ZEKA_IEP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ZEKA_IEP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ZEKA_IEP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ZEKA_IEP] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ZEKA_IEP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ZEKA_IEP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET ARITHABORT OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ZEKA_IEP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ZEKA_IEP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ZEKA_IEP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ZEKA_IEP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET RECOVERY FULL 
GO
ALTER DATABASE [ZEKA_IEP] SET  MULTI_USER 
GO
ALTER DATABASE [ZEKA_IEP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ZEKA_IEP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ZEKA_IEP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ZEKA_IEP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ZEKA_IEP] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ZEKA_IEP', N'ON'
GO
ALTER DATABASE [ZEKA_IEP] SET QUERY_STORE = OFF
GO
USE [ZEKA_IEP]
GO
/****** Object:  Table [dbo].[Auction]    Script Date: 2/2/2019 5:49:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auction](
	[auction_id] [uniqueidentifier] NOT NULL,
	[title] [nchar](100) NOT NULL,
	[picture] [varbinary](max) NOT NULL,
	[duration] [bigint] NOT NULL,
	[starting_price] [decimal](10, 2) NOT NULL,
	[current_price] [decimal](10, 2) NOT NULL,
	[created] [datetime] NULL,
	[opened] [datetime] NULL,
	[closed] [datetime] NULL,
	[state] [nvarchar](10) NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[tokenValue] [decimal](10, 2) NOT NULL,
	[currency] [nvarchar](50) NOT NULL,
	[token_price] [int] NOT NULL,
 CONSTRAINT [PK_Auction] PRIMARY KEY CLUSTERED 
(
	[auction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bid]    Script Date: 2/2/2019 5:49:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bid](
	[bid_id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[auction_id] [uniqueidentifier] NOT NULL,
	[tokens] [int] NOT NULL,
	[time] [datetime] NOT NULL,
	[winner] [bit] NOT NULL,
 CONSTRAINT [PK_Bid] PRIMARY KEY CLUSTERED 
(
	[bid_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemConf]    Script Date: 2/2/2019 5:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConf](
	[system_conf_id] [int] IDENTITY(1,1) NOT NULL,
	[silver_pack] [int] NOT NULL,
	[gold_pack] [int] NOT NULL,
	[platinum_pack] [int] NOT NULL,
	[mrp_group] [int] NOT NULL,
	[auction_duration] [bigint] NOT NULL,
	[currency] [nvarchar](50) NOT NULL,
	[token_value] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[system_conf_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TokenOrder]    Script Date: 2/2/2019 5:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenOrder](
	[token_order_id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[tokens] [int] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[state] [nchar](10) NOT NULL,
	[currency] [nchar](10) NOT NULL,
 CONSTRAINT [PK_TokenOrder] PRIMARY KEY CLUSTERED 
(
	[token_order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/2/2019 5:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [uniqueidentifier] NOT NULL,
	[first_name] [nchar](50) NOT NULL,
	[last_name] [nchar](50) NOT NULL,
	[email] [nchar](50) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[tokens] [bigint] NOT NULL,
	[admin_flag] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Auction] ADD  CONSTRAINT [DF_Auction_auction_id]  DEFAULT (newid()) FOR [auction_id]
GO
ALTER TABLE [dbo].[Bid] ADD  CONSTRAINT [DF_Bid_bid_id]  DEFAULT (newid()) FOR [bid_id]
GO
ALTER TABLE [dbo].[TokenOrder] ADD  CONSTRAINT [DF_TokenOrder_token_order_id]  DEFAULT (newid()) FOR [token_order_id]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_user_id]  DEFAULT (newid()) FOR [user_id]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_admin_flag]  DEFAULT ((0)) FOR [admin_flag]
GO
ALTER TABLE [dbo].[Auction]  WITH CHECK ADD  CONSTRAINT [FK_Auction_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Auction] CHECK CONSTRAINT [FK_Auction_User]
GO
ALTER TABLE [dbo].[Bid]  WITH CHECK ADD  CONSTRAINT [FK_Bid_Auction] FOREIGN KEY([auction_id])
REFERENCES [dbo].[Auction] ([auction_id])
GO
ALTER TABLE [dbo].[Bid] CHECK CONSTRAINT [FK_Bid_Auction]
GO
ALTER TABLE [dbo].[Bid]  WITH CHECK ADD  CONSTRAINT [FK_Bid_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Bid] CHECK CONSTRAINT [FK_Bid_User]
GO
ALTER TABLE [dbo].[TokenOrder]  WITH CHECK ADD  CONSTRAINT [FK_TokenOrder_TokenOrder] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[TokenOrder] CHECK CONSTRAINT [FK_TokenOrder_TokenOrder]
GO
USE [master]
GO
ALTER DATABASE [ZEKA_IEP] SET  READ_WRITE 
GO
