create proc [dbo].[Payment_Delete] (
    @id int    	
) as
begin
	delete from dbo.Payment
	where Id = @id
end