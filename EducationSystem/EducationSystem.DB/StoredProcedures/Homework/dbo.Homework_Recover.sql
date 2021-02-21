CREATE proc [dbo].[Homework_Recover] (
	@id int
	)
 as
begin
	update dbo.Homework
	set
		IsDeleted = 0
	where Id = @id
end