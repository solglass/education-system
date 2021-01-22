create proc [dbo].[HomeworkAttempt_Attachment_SelectAll]
as
begin
	select 
	haa.Id,
	haa.HomeworkAttemptId,
	haa.AttachmentID,
	ha.UserId,
	ha.HomeworkId,
	a.AttachmentTypeId
	from [dbo].[HomeworkAttempt_Attachment] haa 
		inner join dbo.HomeworkAttempt ha on haa.HomeworkAttemptID = ha.Id
		inner join dbo.Attachment a on haa.AttachmentID = a.Id
end
