create proc [dbo].[Lesson_Theme_Delete](@Id int)
as
begin
Delete from dbo.Lesson_Theme where Id = @Id
end