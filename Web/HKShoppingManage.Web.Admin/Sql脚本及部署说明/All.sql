USE [master]
GO
/****** Object:  Database [NewCommerce]    Script Date: 09/06/2018 09:55:57 ******/
CREATE DATABASE [NewCommerce] ON  PRIMARY 
( NAME = N'NewCommerce', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\NewCommerce.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NewCommerce_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\NewCommerce_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [NewCommerce] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NewCommerce].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NewCommerce] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [NewCommerce] SET ANSI_NULLS OFF
GO
ALTER DATABASE [NewCommerce] SET ANSI_PADDING OFF
GO
ALTER DATABASE [NewCommerce] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [NewCommerce] SET ARITHABORT OFF
GO
ALTER DATABASE [NewCommerce] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [NewCommerce] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [NewCommerce] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [NewCommerce] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [NewCommerce] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [NewCommerce] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [NewCommerce] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [NewCommerce] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [NewCommerce] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [NewCommerce] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [NewCommerce] SET  DISABLE_BROKER
GO
ALTER DATABASE [NewCommerce] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [NewCommerce] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [NewCommerce] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [NewCommerce] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [NewCommerce] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [NewCommerce] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [NewCommerce] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [NewCommerce] SET  READ_WRITE
GO
ALTER DATABASE [NewCommerce] SET RECOVERY FULL
GO
ALTER DATABASE [NewCommerce] SET  MULTI_USER
GO
ALTER DATABASE [NewCommerce] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [NewCommerce] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'NewCommerce', N'ON'
GO
USE [NewCommerce]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vendors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VendorName] [varchar](50) NULL,
	[HelpCode] [varchar](20) NULL,
	[VendorAddress] [nvarchar](500) NULL,
	[VendorTel] [varchar](20) NULL,
	[VendorFax] [varchar](20) NULL,
	[VendorEmail] [varchar](20) NULL,
	[Remark] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[LoginCode] [nvarchar](50) NULL,
	[LoginPwd] [nvarchar](100) NULL,
	[IsValid] [bit] NULL,
	[LastLoginIP] [nvarchar](20) NULL,
	[LastLoginTime] [datetime] NULL,
	[Remark] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[Creator] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[Updator] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](500) NULL,
	[HelpCode] [varchar](20) NULL,
	[CategoryId] [int] NULL,
	[StockQty] [numeric](18, 2) NULL,
	[CategoryName] [nvarchar](100) NULL,
	[IsDeleted] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[Creator] [varchar](20) NULL,
	[UpdateTime] [datetime] NULL,
	[Updator] [varchar](20) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Proc_CommonPagingStoredProcedure]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------
--  desc: 通用分页存储过程			
---------------------------------------------------

CREATE PROCEDURE [dbo].[Proc_CommonPagingStoredProcedure]
@Tables nvarchar(1000),			--表名,多表请使用 tableA a inner join tableB b On a.AID = b.AID
@PK nvarchar(100),				--主键，可以带表头 a.AID
@Sort nvarchar(200) = '',		--排序字段
@PageNumber int = 1,			--开始页码
@PageSize int = 10,				--页大小
@Fields nvarchar(1000) = '*',	--读取字段
@Filter nvarchar(1000) = NULL,	--Where条件
@isCount bit = 0  ,   --1		--是否获得总记录数
@Total	int output
AS

DECLARE @strFilter nvarchar(2000)
declare @sql Nvarchar(max)
IF @Filter IS NOT NULL AND @Filter != ''
  BEGIN
   SET @strFilter = ' WHERE 1=1 ' + @Filter + ' '
  END
ELSE
  BEGIN
   SET @strFilter = ' '
  END
if @isCount = 1 --获得记录条数
    begin
		Declare @CountSql Nvarchar(max) 
		Set @CountSql = 'SELECT @TotalCount= Count(1) FROM ' + @Tables + @strFilter 
		Execute sp_executesql @CountSql,N'@TotalCount int output',@TotalCount= @Total Output 
    end
    
if @Sort is null or @Sort = ''''
  set @Sort = @PK + ' DESC '

IF @PageNumber < 1
  SET @PageNumber = 1

if @PageNumber = 1 --第一页提高性能
begin 
  set @sql = 'select top ' + str(@PageSize) +' '+@Fields+ '  from ' + @Tables + ' ' + @strFilter + ' ORDER BY  '+ @Sort 
end 
else
  begin   
	DECLARE @START_ID varchar(50)
	DECLARE @END_ID varchar(50) 


	SET @START_ID = convert(varchar(50),(@PageNumber - 1) * @PageSize + 1)
	SET @END_ID = convert(varchar(50),@PageNumber * @PageSize)
    set @sql =  ' SELECT * '+
   'FROM (SELECT ROW_NUMBER() OVER(ORDER BY '+@Sort+') AS rownum, 
     '+@Fields+ '
      FROM '+@Tables+ @strFilter +' ) AS D
   Where rownum >= '+@START_ID+' AND  rownum <=' +@END_ID +' ORDER BY '+substring(@Sort,charindex('.',@Sort)+1,len(@Sort)-charindex('.',@Sort))
  END
 

EXEC(@sql)
GO
/****** Object:  Table [dbo].[POOrderDetail]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POOrderDetail](
	[Id] [int] NOT NULL,
	[EntryId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[ProductName] [nvarchar](50) NULL,
	[CustomerId] [int] NULL,
	[CustomerName] [nvarchar](50) NULL,
	[Qty] [int] NULL,
	[Price] [float] NULL,
	[ExRate] [float] NULL,
	[TaxPrice] [float] NULL,
	[SalePrice] [float] NULL,
 CONSTRAINT [PK_POOrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[EntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POOrder]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[POOrderNo] [nvarchar](50) NULL,
	[ExchangeRate] [float] NULL,
	[TotalAmount] [float] NULL,
	[TotalProfit] [float] NULL,
	[Creator] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[Updator] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_POOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhotoContent] [image] NULL,
	[Thumbnail] [image] NULL,
	[PhotoDesc] [nvarchar](200) NULL,
	[AlbumId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[Creator] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[Updator] [nvarchar](50) NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CusName] [nvarchar](50) NULL,
	[CusGender] [bit] NULL,
	[CusTelephone] [varchar](20) NULL,
	[CusAddress] [nvarchar](100) NULL,
	[HelpCode] [varchar](20) NULL,
	[CusRemark] [nvarchar](100) NULL,
	[IsValid] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[Creator] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[Updator] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](500) NULL,
	[ParentCategoryId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[Creator] [varchar](20) NULL,
	[UpdateTime] [datetime] NULL,
	[Updator] [varchar](20) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Albums]    Script Date: 09/06/2018 09:55:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Albums](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CoverImgURL] [varchar](50) NULL,
	[AlbumName] [varchar](20) NULL,
	[PhotoNum] [int] NULL,
	[AlbumDesc] [nvarchar](200) NULL,
	[IsDeleted] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[Creator] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[Updator] [nvarchar](50) NULL,
 CONSTRAINT [PK_Albums] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Profiles]    Script Date: 10/20/2018 19:25:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Profiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileNo] [varchar](10) NULL,
	[EmpName] [varchar](20) NULL,
	[EmpIDNo] [varchar](20) NULL,
	[EmpTelNo] [varchar](20) NULL,
	[IsDimissioned] [bit] NULL,
	[RelationVal] [int] NULL,
	[CreateTime] [datetime] NULL,
	[Creator] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[Updator] [nvarchar](50) NULL,
 CONSTRAINT [PK_Profiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmpProfileRelation]    Script Date: 10/20/2018 16:00:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpProfileRelation](
	[ProfileId] [int] NOT NULL,
	[InfoId] [int] NOT NULL,
	[Remark] [nvarchar](1000) NULL
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Profiles_IsDimissioned]    Script Date: 10/20/2018 16:00:11 ******/
ALTER TABLE [dbo].[Profiles] ADD  CONSTRAINT [DF_Profiles_IsDimissioned]  DEFAULT ((0)) FOR [IsDimissioned]
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Proc_GenerateBillNo]
@BillNo VARCHAR(15) OUTPUT AS

DECLARE @maxid INT
DECLARE @prefix VARCHAR(6)
SET @maxid = IDENT_CURRENT('Profiles');
SET @prefix = 'HX-QT-';
SELECT @BillNo = @prefix + REPLICATE('0',5-DATALENGTH(CONVERT(VARCHAR,@maxid))) + CONVERT(VARCHAR,@maxid + 1)