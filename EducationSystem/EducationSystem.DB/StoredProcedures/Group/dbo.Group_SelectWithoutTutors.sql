create proc [dbo].[Group_SelectWithoutTutors]
as
begin
	select
		g.Id,
		g.CourseID,
		g.StatusId,
		g.StartDate
from dbo.[Group] g left join dbo.Tutor_Group tg on tg.GroupID != g.Id
	
end

