CREATE PROCEDURE [dbo].[Comment_SelectByHomeworkAttemptId]
	@attemptId int
AS
	select
		c.Id,
		c.HomeworkAttemptId,
		c.Message,
		u.Id,
		u.FirstName,
		u.LastName,
		u.Login,
		u.UserPic,
		a.Id,
		a.Path,
		at.Id,
		at.Name
	from dbo.Comment c
		inner join dbo.[User] u on c.UserID = u.Id
		inner join dbo.HomeworkAttempt hwa on c.HomeworkAttemptId = hwa.Id
		inner join dbo.Homework hw on hwa.HomeworkId = hw.Id
		left join dbo.Comment_Attachment ca on c.Id = ca.CommentId
		left join dbo.Attachment a on ca.AttachmentId = a.Id
		left join dbo.AttachmentType at on at.Id = a.AttachmentTypeId
	where c.IsDeleted = 0 and c.HomeworkAttemptId = @attemptId
