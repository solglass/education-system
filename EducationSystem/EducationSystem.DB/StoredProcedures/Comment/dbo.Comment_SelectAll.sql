CREATE proc [dbo].[Comment_SelectAll]
as
begin
	select c.Id,
		c.HomeworkAttemptId,
		c.Message,
		u.Id,
		u.FirstName,
		u.LastName
		from dbo.Comment c inner join [dbo].[User] u on c.UserId=u.Id
		where c.IsDeleted=0
end