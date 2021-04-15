CREATE proc [dbo].[Course_Material_Add](
	@courseId int, 
	@MaterialId int
) as
begin
	insert into dbo.Course_Material( CourseID, MaterialID)
	values(@courseId, @MaterialId)
	select SCOPE_IDENTITY()
end
