CREATE proc [dbo].[HomeworkAttempt_SelectByGroupIdAndStatusId]
(@statusId int,
@groupId int)
as
begin
	SELECT
        ha.Id,
		ha.Comment,
		ha.IsDeleted,
		ha.StatusId,
		(select count(*) from dbo.Comment c where c.HomeworkAttemptId = ha.Id) as CountComments,
		(select count(*) from dbo.HomeworkAttempt_Attachment haa where haa.HomeworkAttemptId = ha.Id) as CountAttachments,
		ha.StatusId,
		hw.Id,
		hw.Description,
		u.Id,
		u.FirstName,
		u.LastName,
		u.UserPic
		
    from HomeworkAttempt ha
        join dbo.[User] u on ha.UserId = u.Id
		join dbo.Homework hw on ha.HomeworkId = hw.Id
		
		left join dbo.Homework_Group hg on hw.id = hg.HomeworkId
		left join dbo.[Group] g on hg.GroupID = g.id
    where ha.IsDeleted = 0 and g.Id = @groupId and hwas.Id = @statusId
end
