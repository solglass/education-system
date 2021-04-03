CREATE  proc [dbo].[Comment_SelectById]
(@id int)
as
begin
	select c.Id,
		c.Message,
		c.IsDeleted,
		ha.Id,
		u.Id,
		u.FirstName,
		u.LastName
		from dbo.Comment c 
		inner join [dbo].[User] u on c.UserId=u.Id
		left join [dbo].[HomeworkAttempt] ha on c.HomeworkAttemptId = ha.Id
		where c.Id=@id
end