create proc [dbo].[Role_SelectById](
	@id int
) as
begin
	select Id, Name from dbo.Role where Id = @id
end