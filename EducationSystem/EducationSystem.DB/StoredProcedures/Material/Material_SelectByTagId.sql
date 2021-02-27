CREATE proc dbo.Material_SelectByTagId (
@tagId int
)
as
begin
	select
		m.Id,
		m.Description,
		m.Link
	from dbo.Tag t
		inner join dbo.Material_Tag mt on t.Id = mt.TagId
		inner join dbo.Material m on mt.MaterialId = m.Id
	where m.IsDeleted = 0 and t.Id = @tagId
end