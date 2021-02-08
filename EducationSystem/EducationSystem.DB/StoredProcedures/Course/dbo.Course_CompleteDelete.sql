create proc [dbo].[Course_CompleteDelete] (
	@id int
) as
begin
	delete from dbo.Course
	where Id = @id
end