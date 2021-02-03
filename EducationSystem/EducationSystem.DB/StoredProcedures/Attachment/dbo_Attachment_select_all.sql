create proc dbo.Attachment_SelectAll as
begin
	select 
		a.Id, 
		a.Path,
		a.AttachmentTypeID,
		at.Id,
		at.Name

	from [dbo].[Attachment] a
	inner join [dbo].[AttachmentType] at 
	on a.AttachmentTypeID = at.Id	
end 