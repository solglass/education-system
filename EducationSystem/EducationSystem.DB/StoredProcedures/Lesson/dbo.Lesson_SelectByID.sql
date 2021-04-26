create proc [dbo].[Lesson_SelectById](@id int)
as
begin
select l.Id,
	l.GroupID,
    l.Description,
    l.Date,
	l.RecordLink,
    l.IsDeleted as IsDeleted,
	t.Id,
	t.Name
	from dbo.Lesson l 
		left join dbo.Lesson_Theme lt on l.Id = lt.LessonID
		left join dbo.Theme t on t.Id = lt.ThemeID
	where l.Id = @id
end