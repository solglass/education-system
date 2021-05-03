CREATE proc [dbo].[Group_SelectByTheme] (
@themeId int)
as
begin
select
	 g.StartDate,
	 t.CountLessons,
		t.GroupID,
		c.Id,
		c.Name,
		c.Description,
		c.Duration,
		g.StatusId as Id
from
	(select 
	l.GroupID,
	count (*) as CountLessons
		from dbo.Lesson_Theme lt
			inner join dbo.Lesson l on lt.LessonID = l.Id
		where lt.ThemeID = @themeId and l.Date < SYSDATETIME()
		group by l.GroupID) as t
		inner join dbo.[Group] g on g.Id = t.GroupId
		inner join dbo.[Course] c on c.Id = g.CourseId
end
