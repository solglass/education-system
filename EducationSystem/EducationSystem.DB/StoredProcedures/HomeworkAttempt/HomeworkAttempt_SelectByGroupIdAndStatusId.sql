CREATE proc [dbo].[HomeworkAttempt_SelectByGroupIdAndStatusId]
(@Statusid int,
@GroupId int)
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
		u.UserPic,
		g.Id

    from HomeworkAttempt ha
        join dbo.[User] u on ha.UserId = u.Id
		join dbo.HomeworkAttemptStatus hwas on ha.StatusId = hwas.Id
		join dbo.Homework hw on ha.HomeworkId = hw.Id
        join dbo.[Group] g on hw.GroupID = g.Id 
    where ha.IsDeleted = 0 and g.Id = @GroupId and hwas.Id = @Statusid
end
