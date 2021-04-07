CREATE PROCEDURE [dbo].[Lesson_Theme_Add](
	@ThemeID int,
	@LessonID int)
as
begin
	Insert Into dbo.Lesson_Theme(ThemeID,LessonID) Values(@ThemeID,@LessonID)
	select SCOPE_IDENTITY()
end