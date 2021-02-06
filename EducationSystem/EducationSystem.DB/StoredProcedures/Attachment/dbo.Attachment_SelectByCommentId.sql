create proc [dbo].[Attachment_SelectByCommentId] (
@id int
) as 
begin
	select 
		a.Id, 
		a.Path,
		at.Id,
		at.Name
	from [dbo].[Comment_Attachment] ca 
		inner join [dbo].[Attachment] a on a.Id = ca.AttachmentID
		inner join [dbo].[AttachmentType] at on a.AttachmentTypeID = at.Id
	where ca.[CommentId] = @id
end