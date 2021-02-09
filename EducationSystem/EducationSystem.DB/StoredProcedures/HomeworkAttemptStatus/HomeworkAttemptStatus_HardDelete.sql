create proc [dbo].[HomeworkAttemptStatus_HardDelete]
(
	@id int
)
as
begin
	delete dbo.[HomeworkAttemptStatus] WHERE Id=@id
end