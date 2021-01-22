create proc dbo.Group_Material_Delete (
	@id int
) as
begin
	delete from dbo.Group_Material
	where Id = @id
end