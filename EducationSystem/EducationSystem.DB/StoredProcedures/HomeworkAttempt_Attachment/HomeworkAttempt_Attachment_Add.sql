create proc [dbo].[HomeworkAttempt_Attachment_Add] (
@homeworkAttemptId int,
@attachmentId int)
as
begin
	insert into [dbo].[HomeworkAttempt_Attachment](HomeworkAttemptId,AttachmentId)
	values (@homeworkAttemptId, @attachmentId)
	select SCOPE_IDENTITY()
end
