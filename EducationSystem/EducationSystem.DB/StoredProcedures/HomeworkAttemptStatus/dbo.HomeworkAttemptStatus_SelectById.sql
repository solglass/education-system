CREATE proc [dbo].[HomeworkAttemptStatus_SelectById](
@id int)
as
begin
	select
		Id,
		Name
		from dbo.HomeworkAttemptStatus
		where Id = @id
end