create proc [dbo].[Lesson_Theme_SelectByID](@id int)
as
begin
select
	lt.Id,
    lt.ThemeID,
	t.Name,
	lt.LessonID
	from dbo.Lesson_Theme lt inner join dbo.Theme t on  lt.ThemeID = t.Id
	where lt.Id = @id
end