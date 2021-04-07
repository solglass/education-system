CREATE  proc [dbo].[Comment_SelectById]
(@id int)
as
begin
	select c.Id,
		c.Message,
		c.IsDeleted,
		ha.Id,
		ha.Comment,
		ha.IsDeleted,
		u.Id,
		u.FirstName,
		u.LastName,
		a.Id,
		a.Description,
		a.Path,
		att.Id
		from dbo.Comment c 
		inner join [dbo].[User] u on c.UserId=u.Id
		left join [dbo].[HomeworkAttempt] ha on c.HomeworkAttemptId = ha.Id
		left join [dbo].[Comment_Attachment] ca on ca.CommentId = c.Id 
		left join [dbo].[Attachment] a on ca.AttachmentId = a.Id 
		left join [dbo].[AttachmentType] att on a.AttachmentTypeId = att.Id 
		where c.Id=@id
end