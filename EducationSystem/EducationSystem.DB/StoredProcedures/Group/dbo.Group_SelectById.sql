create proc [dbo].[Group_SelectById] (
	@id int
) as
begin
	select
		g.Id,
		g.StartDate,
		c.Name,
		g.StatusId as Id
from dbo.[Group] g inner join dbo.Course c on g.CourseID = c.Id
	where g.Id = @id
end

