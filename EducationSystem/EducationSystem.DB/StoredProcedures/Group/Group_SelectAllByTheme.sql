CREATE proc [dbo].[Group_SelectAllByTheme] (
@themeId int)
as
begin
select
	 t.*,
	 g.StartDate,
	 c.Name,
	 gs.Name 
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
		inner join dbo.[GroupStatus] gs on gs.Id = g.StatusId
end
