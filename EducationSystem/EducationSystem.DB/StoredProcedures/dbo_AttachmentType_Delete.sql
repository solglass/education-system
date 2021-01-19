create  proc [dbo].[AttachmentType_Delete] (
@id int
) as
begin
	delete from [dbo].[AttachmentType] 
	where Id = @id
end