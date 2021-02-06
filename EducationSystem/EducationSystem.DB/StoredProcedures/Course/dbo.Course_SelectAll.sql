CREATE proc [dbo].[Course_SelectAll]
as
begin 
		select
		c.Id,
		c.Name,
		c.Description,
		c.Duration,
		t.Id,
		t.Name
	from dbo.Course c left join dbo.Course_Theme ct on ct.CourseID = c.Id
		left join dbo.Theme t on   ct.ThemeID=t.Id
	where IsDeleted=0
end