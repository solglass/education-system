create proc [dbo].[Lesson_Theme_Update]( 
@id int,
@ThemeID int,
@LessonID int)
as
begin
Update dbo.Lesson_Theme Set ThemeID = @ThemeID, LessonID = @LessonID where Id=@id
end