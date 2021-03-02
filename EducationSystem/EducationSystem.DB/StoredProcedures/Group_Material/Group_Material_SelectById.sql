CREATE proc [dbo].[Group_Material_SelectById] (
@Id int 
) as
begin 
	select
		Id,
		GroupId,
		MaterialId
		from dbo.Group_Material
		where Id=@id
end
