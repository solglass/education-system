create proc [dbo].[Attachment_SelectByHomeworkAttemptId] (
@id int
) as 
begin
	select 
		a.Id, 
		a.Path,
		at.Id as AttachmentType,
		at.Name
	from dbo.HomeworkAttempt_Attachment haa 
		inner join [dbo].[Attachment] a on a.Id = haa.AttachmentID
		inner join [dbo].[AttachmentType] at on a.AttachmentTypeID = at.Id
	where haa.HomeworkAttemptID = @id
end
