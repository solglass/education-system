CREATE proc [dbo].[HomeworkAttemptStatus_SelectAll]
as
begin
	select
		Id,
		Name
		from dbo.HomeworkAttemptStatus
end