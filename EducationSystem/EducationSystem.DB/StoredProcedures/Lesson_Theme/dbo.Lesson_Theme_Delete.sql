create proc [dbo].[Lesson_Theme_Delete]
@lessonId int,
@themeId int 
as
Delete from dbo.Lesson_Theme where LessonID = lessonId and ThemeID = themeId
