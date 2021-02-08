create proc [dbo].[Lesson_Theme_SelectAll]
as
begin
select
	lt.Id,
    lt.ThemeID,
	t.Name,
	lt.LessonID
	from dbo.Lesson_Theme lt inner join dbo.Theme t on  lt.ThemeID = t.Id
end