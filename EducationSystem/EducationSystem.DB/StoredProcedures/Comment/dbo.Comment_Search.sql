Create proc [dbo].[Comment_Search]
@homeworkAttemptId int = null,
@homeworkId int = null
as
begin
	select c.Id,
		c.Message,
		ha.Id,
		u.Id,
		u.FirstName,
		u.LastName,
		a.Id,
		a.Description,
		a.Path,
		att.Id
		from dbo.Comment c 
		left join [dbo].[User] u on c.UserId=u.Id
		left join [dbo].[HomeworkAttempt] ha on c.HomeworkAttemptId = ha.Id
		left join [dbo].[Comment_Attachment] ca on ca.CommentId = c.Id 
		left join [dbo].[Attachment] a on ca.AttachmentId = a.Id 
		left join [dbo].[AttachmentType] att on a.AttachmentTypeId = att.Id 
		where c.IsDeleted = 0 and
		(@homeworkAttemptId is not null and ha.Id = @homeworkAttemptId or @homeworkAttemptId is null) and 
		(@homeworkId is not null and ha.HomeworkId = @homeworkId or @homeworkId is null)
end