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
 COUNT(c.Id) as [CountAttachments],
 COUNT(a.Id) as [CountComments],
hwas.Id as AttemptStatusId,
hwas.Name as AttemptStatusName,
hw.Id,
hw.Description,
g.Id,
u.Id,
u.FirstName,
u.LastName

from HomeworkAttempt ha
left join [Comment] c on ha.Id = c.HomeworkAttemptId 
left join dbo.HomeworkAttempt_Attachment hwaa on hwaa.HomeworkAttemptID = ha.Id
left join dbo.Attachment a on hwaa.AttachmentID = a.Id
join [User] u on ha.UserId = u.Id
join dbo.HomeworkAttemptStatus hwas on ha.StatusId = hwas.Id
join dbo.Homework hw on ha.HomeworkId = hw.Id
join dbo.[Group] g on hw.GroupID = g.Id 
where ha.IsDeleted = 0 and g.Id = 2 and hwas.Id = 2
GROUP BY ha.Id,
ha.Comment,
ha.IsDeleted,
ha.StatusId,
hwas.Id,
hwas.Name,
hw.Id,
hw.Description,
u.Id,
u.FirstName,
u.LastName,
g.Id
end
