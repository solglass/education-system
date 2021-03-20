create proc [dbo].[Group_Material_Delete] (
	@groupId int,
	@materialId int
) as
begin
	delete from dbo.Group_Material
	where GroupID = @groupId and MaterialID = @materialId
end