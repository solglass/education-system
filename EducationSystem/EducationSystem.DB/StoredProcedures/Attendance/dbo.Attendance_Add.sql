create proc [dbo].[Attendance_Add](
@LessonId int, 
@UserId int,
@IsAbsent bit,
@ReasonOfAbsence nvarchar)
as
begin
Insert Into dbo.Attendance(LessonId,UserId,IsAbsent,ReasonOfAbsence) Values(@LessonId,@UserId,@IsAbsent,@ReasonOfAbsence)
end