CREATE proc [dbo].[HomeworkAttempt_Delete] (
	@id int
)
as
begin
	update dbo.HomeworkAttempt
	set
		IsDeleted = 1
	where Id = @id
end