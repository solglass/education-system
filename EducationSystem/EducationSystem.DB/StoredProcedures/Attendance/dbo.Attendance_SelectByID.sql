create proc [dbo].[Attendance_SelectByID](@id int)
as
begin
select a.Id,
    a.LessonId,
    a.UserId,
    a.IsAbsent,
	u.Id,
	u.FirstName,
	u.LastName,
	u.UserPic
	from dbo.Attendance as a inner join dbo.[User] as u on a.UserId = u.Id
	where a.Id = @id
end