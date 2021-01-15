create proc dbo.Attachment_Update (
@id int,
@path nvarchar(250),
@attachmentTypeID int
) as
begin
	update [dbo].[Attachment] 
	set 
		Path = @path,
		AttachmentTypeID = @attachmentTypeID 
	where Id = @id
end 