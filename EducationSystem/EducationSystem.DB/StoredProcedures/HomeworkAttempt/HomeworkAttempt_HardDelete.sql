create proc [dbo].[HomeworkAttempt_HardDelete]
(
	@id int
)
as
begin
	delete dbo.[HomeworkAttempt] WHERE Id=@id
end