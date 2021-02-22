create proc [dbo].[Lesson_SelectByThemeId](
@themeId int) as
begin
	select
		l.Id,
		l.Date,
		l.Description,
		l.GroupID
	from dbo.Lesson_Theme lt
		inner join dbo.Lesson l on lt.LessonID=l.Id
	where lt.ThemeID=@themeId and l.IsDeleted!=1
end