CREATE proc [dbo].[HomeworkAttempt_Attachment_Delete] (
@homeworkAttemptID int, @attachmentID int)
as
begin
	delete from [dbo].[HomeworkAttempt_Attachment]
	where HomeworkAttemptID=@homeworkAttemptID and AttachmentID=@attachmentID
end
