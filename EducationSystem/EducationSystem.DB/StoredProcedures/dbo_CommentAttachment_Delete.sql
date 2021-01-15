create proc [dbo].[Comment_Attachment_Delete] (
@id int
) as
begin
	delete from [dbo].[Comment_Attachment] 
	where Id = @id
end 