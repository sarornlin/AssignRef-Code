USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[StripeOrderReceipts_SelectAllByUserId]    Script Date: 7/18/2023 12:56:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Lin, Sarorn
-- Create date: 07/11/2023
-- Description:	Select All Order Receipts by UserId
-- Code Reviewer: 

-- Modified BY: 
-- Modified DATE:
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE Proc [dbo].[StripeOrderReceipts_SelectAllByUserId]
			@UserId int
AS

/*------Test Code-------
	DECLARE @UserId int = 8
	EXECUTE [dbo].[StripeOrderReceipts_SelectAllByUserId]
			@UserId
*/

BEGIN

	SELECT sor.[Id]
		  ,sor.[UserId]
		  ,[CheckoutSessionId]
		  ,[LineItemId]
		  ,sp.[ProductId]
		  ,[AmountDiscount]
		  ,[AmountSubtotal]
		  ,[AmountTax]
		  ,[AmountTotal]
		  ,[Currency]
		  ,[Description]
		  ,[Quantity]
		  ,sor.[DateCreated]
	FROM dbo.StripeOrderReceipts as sor
	JOIN [dbo].[StripeProducts] as sp
		ON sp.Id = sor.ProductId
	WHERE sor.UserId = @UserId

END
GO
