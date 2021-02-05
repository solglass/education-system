create proc [dbo].[Theme_SelectAllLearntByGroupId]
(@groupId int) as
begin
	select
		L.Id,
		L.Date,
		t.Id,
		t.Name
	from [dbo].[Lesson] l 
		inner join dbo.Lesson_Theme lt on lt.LessonID=l.Id
		inner join dbo.Theme t on t.Id=lt.ThemeID
	where l.GroupID=@groupId and l.Date<(SELECT SYSDATETIME())
end