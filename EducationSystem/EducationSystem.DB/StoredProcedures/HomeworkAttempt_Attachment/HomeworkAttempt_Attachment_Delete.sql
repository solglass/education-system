CREATE proc [dbo].[HomeworkAttempt_Attachment_Delete] (
@homeworkAttemptId int, @attachmentId int)
as
begin
	delete from [dbo].[HomeworkAttempt_Attachment]
	where HomeworkAttemptID=@homeworkAttemptId and AttachmentID=@attachmentId
end
