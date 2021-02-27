CREATE proc [dbo].[HomeworkAttempt_SelectByUserId]
(@id int)
as
begin
	SELECT
		ha.Id,
		ha.Comment,
		ha.IsDeleted,
		ha.StatusId,
		(select count(*) from dbo.Comment c where c.HomeworkAttemptId = ha.Id) as CountComments,
		(select count(*) from dbo.HomeworkAttempt_Attachment haa where haa.HomeworkAttemptId = ha.Id) as CountAttachments,
		hwas.Id,
		hwas.Name as AttemptStatusName,
		hw.Id,
		hw.Description,
		u.Id,
		u.FirstName,
		u.LastName,
		u.UserPic
	from HomeworkAttempt ha
		join dbo.[User] u on ha.UserId = u.Id
		join dbo.HomeworkAttemptStatus hwas on ha.StatusId = hwas.Id
		join dbo.Homework hw on ha.HomeworkId = hw.Id
	where ha.IsDeleted = 0 and u.Id = @id

end
