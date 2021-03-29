create proc [dbo].[Attendance_SelectByUserId](@userId int)
as
begin
	select
		a.Id,
		a.LessonId,
		a.IsAbsent,
		a.ReasonOfAbsence,
		l.Id,
		l.Date,
		l.Description
	from dbo.Attendance as a 
		inner join dbo.[User] as u on a.UserId = u.Id
		inner join dbo.Lesson l on a.LessonId = l.Id
	where u.Id = @userId
end