create proc [dbo].[Lesson_HardDelete]
( @Id int )
as
begin
	delete from dbo.Lesson
	where Id=@id
end