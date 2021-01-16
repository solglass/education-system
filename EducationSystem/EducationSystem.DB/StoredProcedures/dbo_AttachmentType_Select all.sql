create proc [dbo].[AttachmentType_SelectAll] as
begin
	select Id,  
	Name 
	from [dbo].[AttachmentType]
end