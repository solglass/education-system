create proc dbo.Group_Delete (
	@id int
) as
begin
	delete from dbo.[Group]
	where Id = @id
end
