CREATE proc [dbo].[Comment_SelectAll]
as
begin
	select c.Id,
		c.Message,
		c.IsDeleted,
		u.Id,
		u.FirstName,
		u.LastName,
		ha.Id,
		ha.HomeworkId,
		ha.Comment,
		ha.IsDeleted,
		has.Id,
		has.[Name]
		from dbo.Comment c 
		inner join [dbo].[User] u on c.UserId=u.Id
		inner join [dbo].[HomeworkAttempt] ha on c.HomeworkAttemptId=ha.Id
		inner join dbo.HomeworkAttemptStatus has on ha.StatusId = has.Id
		where c.IsDeleted=0 and ha.IsDeleted = 0
end