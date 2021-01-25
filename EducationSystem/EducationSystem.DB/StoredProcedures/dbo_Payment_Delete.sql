create proc [dbo].[Payment_Delete] (
    @id int    	
) as
begin
	update dbo.Payment
	set
		IsPaid = 1
	where Id = @id
end