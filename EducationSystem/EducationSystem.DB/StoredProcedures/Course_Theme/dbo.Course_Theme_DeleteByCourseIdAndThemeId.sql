create proc [dbo].[Course_Theme_DeleteByCourseIdAndThemeId](
@courseId int, @themeId int) as
begin
	delete from dbo.Course_Theme
	where CourseID=@courseId and ThemeID=@themeId
end