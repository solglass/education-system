create proc [dbo].[Material_Recover] (
	@id int
) as
begin
	update dbo.Material
	set
		IsDeleted = 0
	where Id = @id
end