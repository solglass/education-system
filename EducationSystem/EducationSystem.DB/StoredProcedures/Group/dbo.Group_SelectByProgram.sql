create proc [dbo].[Group_SelectByProgram] (
	@id int
) as
begin
	select
		g.Id,
		g.StartDate,
		c.Id,
		c.Name,
		t.Id,
		t.Name
from dbo.[Group] g inner join dbo.Course c on g.CourseID = c.Id
	left join dbo.Course_Theme ct on ct.CourseID = c.Id
	left join dbo.Theme t on ct.ThemeID = t.Id
	where g.Id = @id
end

