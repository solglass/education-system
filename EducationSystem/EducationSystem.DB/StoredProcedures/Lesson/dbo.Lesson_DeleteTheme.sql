CREATE PROCEDURE [dbo].[dbo.Lesson_DeleteTheme]
	@lessonId int,
	@themeId int
as
begin
	Delete from dbo.Lesson_Theme where LessonID = @lessonId and ThemeID = @themeId
end
