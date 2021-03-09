create proc [dbo].[Attendance_Add](
  @LessonId int, 
  @UserId int,
  @IsAbsent bit
 )
as
begin
	Insert Into dbo.Attendance(LessonId,UserId,IsAbsent)
	Values(@LessonId,@UserId,@IsAbsent)
	select SCOPE_IDENTITY()
end
