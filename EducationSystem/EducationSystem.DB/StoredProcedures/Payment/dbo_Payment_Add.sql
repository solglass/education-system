create proc dbo.Payment_Add (
    @ContractNumber int,
	@Amount decimal(6,2),
	@Date datetime2(7),
	@Period nvarchar(7),
	@IsPaid bit
 ) as
 begin
	insert into [dbo].[Payment](ContractNumber,Amount,Date,Period,IsPaid)
	values (@ContractNumber, @Amount, @Date, @Period, @IsPaid)
	select SCOPE_IDENTITY()
 end
