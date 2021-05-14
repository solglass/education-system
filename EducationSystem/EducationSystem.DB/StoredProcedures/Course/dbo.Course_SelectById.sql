CREATE proc [dbo].[Course_SelectById] (
@id int
)
as
begin 
	select
		c.Id,
		c.Name,
		c.Description,
		c.Duration,
		c.IsDeleted,
		m.Id,
		m.Description,
		m.Link
	from dbo.Course c 
		left join dbo.Course_Material cm on c.Id = cm.CourseID
		left join dbo.Material m on m.Id = cm.MaterialID
	where c.Id = @id and (m.id is null or m.IsDeleted=0)
end