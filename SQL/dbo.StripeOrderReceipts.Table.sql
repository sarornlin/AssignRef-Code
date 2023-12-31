USE [AssignRef]
GO
/****** Object:  Table [dbo].[StripeOrderReceipts]    Script Date: 7/18/2023 12:56:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StripeOrderReceipts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CheckoutSessionId] [nvarchar](128) NOT NULL,
	[LineItemId] [nvarchar](50) NOT NULL,
	[ProductId] [int] NOT NULL,
	[AmountDiscount] [int] NOT NULL,
	[AmountSubtotal] [int] NOT NULL,
	[AmountTax] [int] NOT NULL,
	[AmountTotal] [int] NOT NULL,
	[Currency] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[Quantity] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_StripeOrderReceipts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StripeOrderReceipts] ADD  CONSTRAINT [DF_Stripe_OrderReciepts_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[StripeOrderReceipts]  WITH CHECK ADD  CONSTRAINT [FK_StripeOrderReceipts_StripeProducts] FOREIGN KEY([ProductId])
REFERENCES [dbo].[StripeProducts] ([Id])
GO
ALTER TABLE [dbo].[StripeOrderReceipts] CHECK CONSTRAINT [FK_StripeOrderReceipts_StripeProducts]
GO
ALTER TABLE [dbo].[StripeOrderReceipts]  WITH CHECK ADD  CONSTRAINT [FK_StripeOrderReceipts_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[StripeOrderReceipts] CHECK CONSTRAINT [FK_StripeOrderReceipts_Users]
GO
