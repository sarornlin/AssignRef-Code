USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[StripeTransaction_Insert]    Script Date: 7/18/2023 12:56:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jacobo Gallego
-- Create date: 06/10/2023
-- Description: Inser a record to keep track of the transactions done trough stripe (Charges and transfers)
-- Code Reviewer: 

-- MODIFIED BY: Sarorn Lin
-- MODIFIED DATE: 6/29/2023
-- Code Reviewer:Geron DeBose
-- Note: Changing parameters to include values needed for Transfer transactions, also adding in a subquery for Type to use the TypeId values instead of a nvarchar.
-- =============================================

CREATE PROC [dbo].[StripeTransaction_Insert]
			@TransactionId nvarchar(50)
			,@SourceId nvarchar(50) = null
			,@DestinationId nvarchar(50) = null
			,@Type nvarchar(50)
			,@Amount int
			,@Description nvarchar(200) = null
			,@Id int OUTPUT

as

/* ------------- Test code ----------------

DECLARE @TransactionId nvarchar(50) = 'transactionId_test'
		,@SourceId nvarchar(50) = 'source_test'
		,@DestinationId nvarchar(50) = 'destination_test'
		,@Type nvarchar(50) = 'transfer'
		,@Amount int = 100
		,@Description nvarchar(200) = 'test description'
		

DECLARE @Id int = 0

EXECUTE [dbo].[StripeTransaction_Insert]
			@TransactionId
			,@SourceId
			,@DestinationId
			,@Type
			,@Amount
			,@Description
			,@Id

SELECT *
FROM dbo.StripeTransactions		

*/

BEGIN

/*Query to get the transaction type number*/

DECLARE @TransactionTypeId int;

SELECT	@TransactionTypeId = stt.Id
		FROM [dbo].[StripeTransactionTypes] as stt
		WHERE stt.Type = @Type

/*Insert transaction into table*/

INSERT INTO [dbo].[StripeTransactions]
           ([TransactionId]
           ,[SourceId]
		   ,[DestinationId]
		   ,[TransactionTypeId]
           ,[Amount]
		   ,[Description])
     VALUES
           (@TransactionId
			,@SourceId
			,@DestinationId
			,@TransactionTypeId
			,@Amount
			,@Description)

SET @Id = SCOPE_IDENTITY()

END

GO
