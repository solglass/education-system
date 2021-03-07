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
		t.Id,
		t.Name
	from dbo.Course c left join dbo.Course_Theme ct on c.Id = ct.CourseID
		left join dbo.Theme t on t.Id = ct.ThemeID
	where c.Id = @id and t.IsDeleted=0
end