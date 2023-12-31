USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[StripeOrderReceipts_SelectAllPaginated]    Script Date: 7/18/2023 12:56:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Lin, Sarorn
-- Create date: 07/11/2023
-- Description:	Select All Order Receipts in Paginated format in descending order
-- Code Reviewer: 

-- Modified BY: 
-- Modified DATE:
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE proc [dbo].[StripeOrderReceipts_SelectAllPaginated]
			@PageIndex int
			,@PageSize int
AS

/*------Test Code-------
	DECLARE @PageIndex int = 0
		,@PageSize int = 5

	EXECUTE [dbo].[StripeOrderReceipts_SelectAllPaginated]
			@PageIndex
			,@PageSize
*/

BEGIN

	DECLARE @Offset int = @PageIndex * @Pagesize
	
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
	  FROM [dbo].[StripeOrderReceipts] as sor
	  JOIN [dbo].[StripeProducts] as sp
	  ON sp.Id = sor.ProductId
	  ORDER BY sor.DateCreated DESC
	  OFFSET @Offset ROWS
	  FETCH NEXT @PageSize ROWS ONLY


END
GO
