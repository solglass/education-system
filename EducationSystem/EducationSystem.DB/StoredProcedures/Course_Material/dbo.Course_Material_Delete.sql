CREATE proc [dbo].[Course_Material_Delete](
	@courseId int,
	@MaterialId int)
as
begin
	delete from dbo.Course_Material
	where CourseID=@courseId and MaterialID=@MaterialId
end
