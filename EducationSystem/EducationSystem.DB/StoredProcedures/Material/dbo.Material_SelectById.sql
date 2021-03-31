create proc [dbo].[Material_SelectById] (
	@id int
) as
begin
	select
	    m.Id,
		m.Link,
		m.Description,
		m.IsDeleted,
		t.Id,
		t.Name
	from dbo.Material m 
		left join dbo.Material_Tag mt on m.Id = mt.MaterialId
		left join dbo.Tag t on t.Id = mt.TagId
	where m.Id = @id
end

