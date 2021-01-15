create proc [dbo].[Comment_Attachment_SelectAll]  as
begin
	select Id, 
 [CommentID],
 [AttachmentID]
	from [dbo].[Comment_Attachment]

end