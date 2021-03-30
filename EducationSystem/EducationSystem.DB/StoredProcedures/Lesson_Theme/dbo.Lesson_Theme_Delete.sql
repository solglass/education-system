CREATE PROCEDURE [dbo].[Lesson_Theme_Delete]
	@lessonId int,
	@themeId int
as
begin
	Delete from dbo.Lesson_Theme where LessonID = @lessonId and ThemeID = @themeId
end
