create proc dbo.Group_SelectAll
as
begin
	select
		g.Id,
		g.StartDate,
		c.Id,
		c.Name,
		c.Description,
		c.Duration,
		g.StatusId as Id
from dbo.[Group] g inner join dbo.Course c on g.CourseID = c.Id
end

