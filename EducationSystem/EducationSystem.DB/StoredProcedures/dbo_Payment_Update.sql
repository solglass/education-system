create proc dbo.Payment_Update (
	@id int,
	@ContractNumber int,
	@Amount decimal(6,2),
	@Date datetime2(7),
	@Period nvarchar(7),
	@IsPaid bit
 ) as
 begin
	update [dbo].[Payment]
		set
		ContractNumber = @ContractNumber,
	Amount = @Amount,
	Date = @Date,
	Period = @Period,
	IsPaid= @IsPaid
	where Id = @id
 end