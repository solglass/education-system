create proc [dbo].[Attendance_Add](
  @LessonId int, 
  @UserId int,
  @IsAbsent bit,
  @reason nvarchar(Max)
 )
as
begin
	Insert Into dbo.Attendance(LessonId,UserId,IsAbsent, ReasonOfAbsence)
	Values(@LessonId,@UserId,@IsAbsent, @reason)
	select SCOPE_IDENTITY()
end
