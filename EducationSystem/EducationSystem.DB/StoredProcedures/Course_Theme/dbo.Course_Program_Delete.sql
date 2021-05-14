CREATE proc [dbo].[Course_Program_Delete](
@courseId int) 
as
begin
	delete from dbo.Course_Theme
	where CourseID=@courseId
end