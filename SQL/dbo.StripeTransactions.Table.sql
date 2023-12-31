USE [AssignRef]
GO
/****** Object:  Table [dbo].[StripeTransactions]    Script Date: 7/18/2023 12:56:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StripeTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[SourceId] [nvarchar](50) NULL,
	[DestinationId] [nvarchar](50) NULL,
	[TransactionTypeId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_StripeTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StripeTransactions] ADD  CONSTRAINT [DF_StripeTransactions_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[StripeTransactions]  WITH CHECK ADD  CONSTRAINT [FK_StripeTransactions_StripeTransactionTypes] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[StripeTransactionTypes] ([Id])
GO
ALTER TABLE [dbo].[StripeTransactions] CHECK CONSTRAINT [FK_StripeTransactions_StripeTransactionTypes]
GO
