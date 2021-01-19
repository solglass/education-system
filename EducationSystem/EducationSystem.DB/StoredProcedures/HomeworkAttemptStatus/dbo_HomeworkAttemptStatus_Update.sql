CREATE proc [dbo].[HomeworkAttemptStatus_Update] (
@id int,
@name nvarchar
)
as
begin
	update dbo.HomeworkAttemptStatus
	set
		Name = @name
	where Id = @id
end