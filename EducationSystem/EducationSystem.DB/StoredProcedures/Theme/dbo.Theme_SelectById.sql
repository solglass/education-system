CREATE proc [dbo].[Theme_SelectById]
(@id int)
as
begin
select 
t.Id,
	t.Name,
	c.Id,
	c.Name,
	c.Description,
	c.Duration,
	h.Id,
	h.StartDate,
	h.DeadlineDate,
	h.Description,
	h.IsOptional,
	h.GroupID,
	tag.Id,
	tag.Name,
	l.Id,
	l.Date,
	l.Description,
	l.GroupID

from dbo.Theme t left join dbo.Course_Theme ct on ct.ThemeID=t.Id
	left join dbo.Course c on c.Id=ct.CourseID
	left join dbo.Homework_Theme ht on ht.ThemeId=t.Id
	left join dbo.Homework h on h.Id=ht.HomeworkId
	left join dbo.Theme_Tag tt on tt.ThemeId=t.Id
	left join dbo.Tag tag on tag.Id=tt.TagId
	left join dbo.Lesson_Theme lt on lt.ThemeID=t.Id
	left join dbo.Lesson l on l.Id=lt.LessonID
	where t.Id=@id
end
