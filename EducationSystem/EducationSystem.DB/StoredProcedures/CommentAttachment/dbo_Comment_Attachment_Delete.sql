create proc [dbo].[Comment_Attachment_Delete](
@commentId int,
@attachmentId int)
as
begin
	delete from [dbo].[Comment_Attachment] 
	where CommentId = @commentId and AttachmentId = @attachmentId
end