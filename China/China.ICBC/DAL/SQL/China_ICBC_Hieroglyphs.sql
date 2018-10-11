USE [Snova_table_ÑÑÑÁ]
GO

/****** Object:  Table [dbo].[China_ICBC_Hieroglyphs]    Script Date: 20.12.2013 19:29:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[China_ICBC_Hieroglyphs](
	[Name] [nvarchar](10) NOT NULL,
	[Hieroglyph] [nchar](1) NOT NULL,
	[HieroglyphCode] [char](4) NOT NULL,
	[RowID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_China_ICBC_Hieroglyphs_RowID] PRIMARY KEY NONCLUSTERED 
(
	[RowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Index [IX_China_ICBC_Hieroglyphs_Name]    Script Date: 20.12.2013 19:31:16 ******/
CREATE CLUSTERED INDEX [IX_China_ICBC_Hieroglyphs_Name] ON [dbo].[China_ICBC_Hieroglyphs]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

