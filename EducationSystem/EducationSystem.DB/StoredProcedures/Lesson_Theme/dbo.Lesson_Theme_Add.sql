create procedure [dbo].[Lesson_Theme_Add]
@lessonId int,
@themeId int
as
begin
Insert Into dbo.Lesson_Theme(LessonID, ThemeID) Values(@lessonId, @themeId)
end
