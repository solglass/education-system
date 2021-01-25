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
		t.Name,
		g.Id,
		g.StartDate,
		gs.Id,
		gs.Name
	from dbo.Course c left join dbo.Course_Theme ct on c.Id = ct.CourseID
		left join dbo.Theme t on t.Id = ct.ThemeID
		left join dbo.[Group] g on g.CourseID=c.Id
		left join dbo.GroupStatus gs on g.StatusId=gs.Id
	where c.Id = @id
end