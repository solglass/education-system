CREATE proc [dbo].[HomeworkAttempt_Recover] (
	@id int
)
as
begin
	update dbo.HomeworkAttempt
	set
		IsDeleted = 0
	where Id = @id
end