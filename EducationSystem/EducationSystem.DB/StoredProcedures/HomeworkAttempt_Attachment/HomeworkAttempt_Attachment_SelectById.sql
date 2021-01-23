create proc [dbo].[HomeworkAttempt_Attachment_SelectById] (
@id int)
as
begin
	select 
	Id,
	HomeworkAttemptID,
	AttachmentID
	from [dbo].[HomeworkAttempt_Attachment] 
		where Id = @id
end