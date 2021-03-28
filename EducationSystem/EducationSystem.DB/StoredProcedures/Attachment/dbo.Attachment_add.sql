create proc [dbo].[Attachment_Add] (
@description nvarchar(max),
@path nvarchar(250), 
@attachmentTypeId int
) as
begin
	insert into [dbo].[Attachment] (Description, Path, AttachmentTypeId) 
	values (@description, @path, @attachmentTypeId )
	select SCOPE_IDENTITY() 
end