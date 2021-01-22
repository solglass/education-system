create proc [dbo].[HomeworkAttempt_Attachment_Add] (
@homeworkAttemptId int,
@attachmentId int)
as
begin
	insert into [dbo].[HomeworkAttempt_Attachment](HomeworkAttemptId,AttachmentId)
	values ((select Id from dbo.HomeworkAttempt where Id = @homeworkAttemptId), (select Id from dbo.Attachment where Id = @attachmentId))
	
	select SCOPE_IDENTITY()
end
