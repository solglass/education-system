create proc [dbo].[Homework_Tag_Delete] (
@id int
) as
begin
	delete from dbo.Homework_Tag
	where Id = @id
end