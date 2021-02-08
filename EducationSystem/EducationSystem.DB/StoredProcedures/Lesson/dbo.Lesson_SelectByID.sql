create proc [dbo].[Lesson_SelectByID](@id int)
as
begin
select l.Id,
	l.GroupID,
    l.Description,
    l.Date,
    l.IsDeleted,
	t.Id,
	t.Name
	from dbo.Lesson l inner join dbo.Lesson_Theme lt on l.Id = lt.LessonID inner join dbo.Theme t on t.Id = lt.ThemeID
	where l.Id = @id and IsDeleted=0
end