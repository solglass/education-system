CREATE PROCEDURE [dbo].[Lesson_Theme_Delete]
    @lessonId int,
    @themeId int 
as
delete from dbo.Lesson_Theme 
    where LessonID =  @lessonId and ThemeID = @themeId