USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[StripeOrderReceipts_Insert]    Script Date: 7/18/2023 12:56:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Sarorn Lin
-- Create date: 07/07/2023
-- Description: Insert a record of an order receipt given by Stripe;
--	If record with Line Item Id already exists, it will return that record's Id
--	If record with Line Item Id does not exist, it will create a new record
-- Code Reviewer: 

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- =============================================

CREATE PROC [dbo].[StripeOrderReceipts_Insert]	
				@UserId int,
				@CheckoutSessionId nvarchar(128),
				@LineItemId nvarchar(50),
				@ProductId nvarchar(50),
				@AmountDiscount int,
				@AmountSubtotal int,
				@AmountTax int,
				@AmountTotal int,
				@Currency nvarchar(50),
				@Description nvarchar(50),
				@Quantity int,
				@Id int OUTPUT
				
as

/********* TEST CODE **************
SELECT *
FROM dbo.StripeOrderReceipts

DECLARE @Id int = 0;

DECLARE  @UserId int = 8
		,@CheckoutSessionId nvarchar(128) = 'cs_test_a1gdfghjhtOxVLVzpBDhRj08FIGlHi0WCL1j6WWMY5tgP8322'
		,@LineItemId nvarchar(50) = 'l363346r'
		,@ProductId nvarchar(50) = 'prod_ODLnZmy2fCkIZC'
		,@AmountDiscount int = 0
		,@AmountSubtotal int = 1000
		,@AmountTax int = 0
		,@AmountTotal int = 1000
		,@Currency nvarchar(50) = 'USD'
		,@Description nvarchar(50) = 'Shirt'
		,@Quantity int = 1

EXECUTE [dbo].[StripeOrderReceipts_Insert]
			@UserId
			,@CheckoutSessionId
			,@LineItemId
			,@ProductId
			,@AmountDiscount
			,@AmountSubtotal
			,@AmountTax
			,@AmountTotal
			,@Currency
			,@Description
			,@Quantity
			,@Id OUTPUT

SELECT *
FROM dbo.StripeOrderReceipts

Select @Id

delete from dbo.StripeOrderReceipts where UserId = 8
		
*/

IF EXISTS (	SELECT [LineItemId]
			FROM [dbo].[StripeOrderReceipts] as s
			WHERE s.LineItemId = @LineItemId)
BEGIN
	SET @Id =
	(SELECT Id 
	FROM [dbo].[StripeOrderReceipts] as s
	WHERE s.LineItemId = @LineItemId)
END

ELSE

BEGIN

DECLARE @ProductTableId nvarchar(50);

SET @ProductTableId = ( SELECT sp.Id
						from dbo.StripeProducts as sp
						where sp.ProductId = @ProductId)

	INSERT INTO [dbo].[StripeOrderReceipts]
			   ([UserId]
			   ,[CheckoutSessionId]
			   ,[LineItemId]
			   ,[ProductId]
			   ,[AmountDiscount]
			   ,[AmountSubtotal]
			   ,[AmountTax]
			   ,[AmountTotal]
			   ,[Currency]
			   ,[Description]
			   ,[Quantity])
		 VALUES
				(@UserId
				,@CheckoutSessionId
				,@LineItemId
				,@ProductTableId
				,@AmountDiscount
				,@AmountSubtotal
				,@AmountTax
				,@AmountTotal
				,@Currency
				,@Description
				,@Quantity)

	SET @Id = SCOPE_IDENTITY()

END
GO
