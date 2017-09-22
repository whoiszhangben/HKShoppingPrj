Use DYZC_MANAGE;
GO
/****** Object:  StoredProcedure [dbo].[Proc_CommonPagingStoredProcedure]    Script Date: 09/06/2016 15:31:19 ******/
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

USE [myCommerce]
GO

/****** Object:  Table [dbo].[User]    Script Date: 09/22/2017 16:30:05 ******/
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