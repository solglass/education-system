create proc [dbo].[Lesson_Delete]
( @Id int )
as
begin
	delete from dbo.Lesson
	where Id=@id
end