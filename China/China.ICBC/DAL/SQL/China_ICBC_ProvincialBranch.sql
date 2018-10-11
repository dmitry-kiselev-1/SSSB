USE [Snova_table_ÑÑÑÁ]
GO

/****** Object:  Table [dbo].[China_ICBC_ProvincialBranch]    Script Date: 13.01.2014 14:56:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[China_ICBC_ProvincialBranch](
	[Name] [varchar](128) NOT NULL,
	[Address] [varchar](1024) NOT NULL,
	[SWIFT] [varchar](11) NOT NULL,
 CONSTRAINT [PK_China_ICBC_ProvincialBranch] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

