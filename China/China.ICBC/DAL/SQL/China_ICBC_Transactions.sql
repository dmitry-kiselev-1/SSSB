USE [Snova_table_СССБ]
GO

/****** Object:  Table [dbo].[China_ICBC_Transactions]    Script Date: 14.01.2014 18:09:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[China_ICBC_Transactions](
	[PaymentSystem] [char](3) NOT NULL,
	[Date] [date] NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[SwiftTransactionNumber] [char](15) NOT NULL,
	[MessageType] [char](1) NOT NULL,
	[MessageSwift] [nvarchar](1024) NOT NULL,
	[Time] [time](0) NOT NULL,
	[MessageObject] [varbinary](max) NOT NULL,
	[IsFileSent] [bit] NOT NULL,
	[Код_проверки_платежа] [int] NULL,
	[Код_платежа] [int] NULL,
 CONSTRAINT [PK_China_ICBC_Transactions] PRIMARY KEY CLUSTERED 
(
	[PaymentSystem] ASC,
	[Date] ASC,
	[OrderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[China_ICBC_Transactions] ADD  CONSTRAINT [DF_China_ICBC_Transactions_FileIsSent]  DEFAULT ((0)) FOR [IsFileSent]
GO

