CREATE proc [dbo].[Homework_Delete] (
	@id int
	)
 as
begin
	update dbo.Homework
	set
		IsDeleted = 1
	where Id = @id
end