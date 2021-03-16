create procedure [dbo].[Lesson_AddTheme]
@lessonId int,
@themeId int
as
begin
Insert Into dbo.Lesson_Theme(LessonID, ThemeID) Values(@lessonId, @themeId)
end
