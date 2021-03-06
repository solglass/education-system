create proc [dbo].[Lesson_SelectByGroupId](@idGroup int)
as
begin
select
	l.Id,
	l.GroupID,
    l.Description,
    l.Date,
	l.RecordLink,
	t.Id,
	t.Name
	from dbo.Lesson l 
		left join dbo.Lesson_Theme lt on l.Id = lt.LessonID 
		left join dbo.Theme t on t.Id = lt.ThemeID
	where l.GroupID=@idGroup
end