create proc [dbo].[Group_SelectWithoutTutors]
as
begin
	select
		g.Id,
		g.StartDate,
		gs.Id,
		gs.Id as GroupStatus,
		c.Id,
		c.Name,
		c.Description,
		c.Duration
from dbo.[Group] g 
	inner join dbo.GroupStatus gs on g.StatusId = gs.Id
	inner join dbo.Course c on g.CourseID = c.Id
	left join dbo.Tutor_Group tg on tg.GroupID = g.Id
	where tg.GroupID is null
end
