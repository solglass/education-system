create  proc [dbo].[Attachment_Delete] (
@id int
) as
begin
	delete from [dbo].[Attachment] 
	where Id = @id
end