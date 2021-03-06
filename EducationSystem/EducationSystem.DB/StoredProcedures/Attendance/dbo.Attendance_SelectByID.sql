create proc [dbo].[Attendance_SelectByID](@id int)
as
begin
select 
	a.Id,
	a.IsAbsent,
	a.ReasonOfAbsence,
	u.Id,
	u.FirstName,
	u.LastName,
	u.Login,
	u.UserPic,
	l.Id,
	l.Description,
	l.Date
	from dbo.Attendance as a 
	inner join dbo.[User] as u on a.UserId = u.Id
	inner join dbo.Lesson l on a.LessonId = l.Id
	where a.Id = @id
end