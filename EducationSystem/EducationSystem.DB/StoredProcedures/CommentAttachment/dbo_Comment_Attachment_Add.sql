create proc [dbo].[Comment_Attachment_Add] (
@commentId int,
@attachmentId int)
 as
begin

	insert into [dbo].[Comment_Attachment] (CommentId, AttachmentId) 
	values (@commentId, @attachmentId)
	select SCOPE_IDENTITY()
end 