create proc [dbo].[Material_Delete] (
	@id int
) as
begin
	update dbo.Material
	set
		IsDeleted = 1
	where Id = @id
end