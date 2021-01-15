create proc dbo.Attachment_SelectAll as
begin
	select Id, 
	Path,
	a.AttachmentTypeID,
	at.Name

	from [dbo].[Attachment] a
	inner join [dbo].[AttachmentType] at 
	on a.AttachmentTypeID = at.Id	
end