CREATE proc dbo.Material_SelectByGroupId (
@groupId int
)
as
begin
	select
		m.Id,
		m.Description,
		m.Link
	from dbo.Group_Material gm
		inner join dbo.Material m on gm.MaterialId = m.Id
	where m.IsDeleted = 0 and gm.GroupID = @groupId
end
