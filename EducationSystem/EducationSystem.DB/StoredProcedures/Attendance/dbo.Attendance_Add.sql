create proc [dbo].[Attendance_Add](
  @lessonId int, 
  @userId int,
  @isAbsent bit,
  @reason nvarchar(Max)
 )
as
begin
	Insert Into dbo.Attendance(LessonId,UserId,IsAbsent, ReasonOfAbsence)
	Values(@lessonId,@userId,@isAbsent, @reason)
	select SCOPE_IDENTITY()
end
