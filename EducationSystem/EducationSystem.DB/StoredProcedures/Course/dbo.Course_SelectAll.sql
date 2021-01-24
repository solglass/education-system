CREATE proc [dbo].[Course_SelectAll]
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
	from dbo.Course c left join dbo.Course_Theme ct on ct.CourseID = c.Id
		left join dbo.Theme t on   ct.ThemeID=t.Id
		left join dbo.[Group] g on g.CourseID = c.Id
		left join dbo.GroupStatus gs on g.StatusId=gs.Id
	where IsDeleted=0
end