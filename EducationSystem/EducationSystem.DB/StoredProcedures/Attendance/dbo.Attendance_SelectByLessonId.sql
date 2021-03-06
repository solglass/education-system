create proc [dbo].[Attendance_SelectByLessonId](@lessonId int)
as
begin
	select
		a.Id,
		a.LessonId,
		a.IsAbsent,
		a.ReasonOfAbsence,
		u.Id,
		u.FirstName,
		u.LastName,
		u.UserPic
	from dbo.Attendance as a 
		inner join dbo.[User] as u on a.UserId = u.Id
	where a.LessonId = @lessonId
end