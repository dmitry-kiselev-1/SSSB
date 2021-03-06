USE [Snova_table_СССБ]
GO
/****** Object:  StoredProcedure [dbo].[spChina_ICBC_Transactions_Insert]    Script Date: 31.12.2013 15:14:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dmitry Kiselev
-- Create date: 2013-12-20
-- Description:	Добавляет сообщение в таблицу транзакций 
--				по переводам в Торгово-промышленный банк Китая
-- Example:		
				/*
 				DECLARE @DateTime DATETIME2(0), @MessageObject varbinary(max)
 				SELECT @DateTime = GETDATE(), @MessageObject = CAST('' AS varbinary)
 				--SELECT @DateTime, @MessageObject
 				exec dbo.spChina_ICBC_Transactions_Insert 'CMT', @DateTime, 'A', '', @MessageObject
				--DELETE China_ICBC_Transactions
				*/ 
-- =============================================
ALTER PROCEDURE [dbo].[spChina_ICBC_Transactions_Insert]
	 @PaymentSystem CHAR(3)
	,@DateTime DATETIME2(0)
	,@MessageType char(1)
	,@MessageSwift nvarchar(1024)
	,@MessageObject varbinary(max)
    
AS
BEGIN
	SET NOCOUNT ON
	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	BEGIN TRAN

		/*
		-- Отладка:
		DECLARE @DateTime DATETIME, @PaymentSystem CHAR(3)
			SELECT @DateTime = GETDATE(), @PaymentSystem = 'CMT'
		*/

		DECLARE @LastOrderNumber INT, @NewOrderNumber INT, @NewOrderNumberLeght INT, @NewSwiftTransactionNumber char(15), @NewMessageSwift nvarchar(1024), @DateOnly DATE, @TimeOnly TIME(0)

		SELECT @DateOnly = @DateTime, @TimeOnly = @DateTime

		SELECT @LastOrderNumber = MAX(OrderNumber) 
			FROM dbo.China_ICBC_Transactions
			WITH(TABLOCKX)
			--WITH(XLOCK)
			WHERE PaymentSystem = @PaymentSystem
			AND [Date] = @DateOnly
		--SELECT @LastOrderNumber 

		SET @NewOrderNumber = ISNULL(@LastOrderNumber, 0) + 1
		SET @NewOrderNumberLeght = LEN( CAST(@NewOrderNumber AS VARCHAR(6)) )
		--SELECT @NewOrderNumberLeght

		SET	@NewSwiftTransactionNumber = 
			@PaymentSystem +
			CONVERT(VARCHAR(6), @DateTime, 12) +
			REPLICATE('0', 6 - @NewOrderNumberLeght) + 
			CONVERT(VARCHAR(6), @NewOrderNumber)

		-- Корректировка номера транзакции (:20:CMT131220000000) внутри сообщения:
		SELECT @NewMessageSwift = LEFT(@MessageSwift, 4) + @NewSwiftTransactionNumber + SUBSTRING(@MessageSwift, 20, LEN(@MessageSwift))

		--SELECT @NewSwiftTransactionNumber, @NewMessageBody, @MessageBody

		INSERT [dbo].[China_ICBC_Transactions]
				   ([PaymentSystem]
				   ,[Date]
				   ,[OrderNumber]
				   ,[SwiftTransactionNumber]
				   ,[MessageType]
				   ,[MessageSwift]
				   ,[Time]
				   ,[Код_проверки_платежа]
				   ,[Код_платежа]
				   ,[MessageObject])
			 OUTPUT INSERTED.*
			 VALUES
				   (@PaymentSystem
				   ,@DateOnly
				   ,@NewOrderNumber
				   ,@NewSwiftTransactionNumber
				   ,@MessageType
				   ,@NewMessageSwift
				   ,@TimeOnly
				   ,DEFAULT
				   ,DEFAULT
				   ,@MessageObject)

	COMMIT

END
