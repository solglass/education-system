create proc [dbo].[Role_Delete](
	@id int
) as
begin
	delete from dbo.Role where Id = @id
end