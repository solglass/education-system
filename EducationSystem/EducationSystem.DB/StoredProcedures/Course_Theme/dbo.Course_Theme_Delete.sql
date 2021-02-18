CREATE proc [dbo].[Course_Theme_Delete](
@courseId int, @themeId int) as
begin
	delete from dbo.Course_Theme
	where CourseID=@courseId and ThemeID=@themeId
end