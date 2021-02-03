CREATE proc [dbo].[Course_Theme_Add](
@courseId int, 
@themeId int
) as
begin
	insert into dbo.Course_Theme( CourseID, ThemeID)
	values(@courseId, @themeId)
	select SCOPE_IDENTITY()
end
