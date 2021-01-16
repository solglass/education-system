CREATE proc [dbo].[HomeworkAttemptStatus_Delete] (
@id int
)
as
begin
	delete from dbo.HomeworkAttemptStatus where Id = @id
end