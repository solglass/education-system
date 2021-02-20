create proc [dbo].[Attachment_Add] (
@path nvarchar(250), 
@attachmentTypeId int
) as
begin
	insert into [dbo].[Attachment] (Path, AttachmentTypeId) 
	values (@path, @attachmentTypeId )
	select SCOPE_IDENTITY() 
end