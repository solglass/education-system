CREATE proc [dbo].[Course_Theme_Add](
@courseId int, 
@themeID int
) as
begin
	insert into dbo.Course_Theme( CourseID, ThemeID)
	values(@courseId, @themeID)
end
