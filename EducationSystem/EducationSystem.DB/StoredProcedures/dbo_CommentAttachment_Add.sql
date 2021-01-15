create proc [dbo].[Comment_Attachment_Add] (
@commentID int,
@attachmentID int)
 as
begin

	insert into [dbo].[Comment_Attachment] (CommentID, AttachmentID) 
	values (@commentID, @attachmentID)
end 