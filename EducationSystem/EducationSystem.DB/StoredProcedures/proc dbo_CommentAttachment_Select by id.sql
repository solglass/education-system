create proc [dbo].[Comment_Attachment_SelectById] (
@id int
) as
begin
	select Id, 
 [CommentID],
 [AttachmentID]
	from [dbo].[Comment_Attachment]
	where Id = @id
end