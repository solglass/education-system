CREATE proc [dbo].[Attendance_Add](
@LessonId int, 
@UserId int,
@IsAbsent bit)
as
begin
	insert into dbo.Attendance(LessonId,UserId,IsAbsent) 
	values(@LessonId,@UserId,@IsAbsent)
	select SCOPE_IDENTITY()
end
