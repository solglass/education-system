create proc dbo.Attachment_Update (
@id int,
@description nvarchar(1000),
@path nvarchar(250),
@attachmentTypeID int
) as
begin
	update [dbo].[Attachment] 
	set 
		Description = @description,
		Path = @path,
		AttachmentTypeID = @attachmentTypeID 
	where Id = @id
end 