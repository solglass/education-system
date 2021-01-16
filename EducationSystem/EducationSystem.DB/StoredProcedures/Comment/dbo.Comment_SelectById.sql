CREATE  proc [dbo].[Comment_SelectById]
(@id int)
as
begin
	select c.Id,
		c.HomeworkAttemptId,
		c.Message,
		c.IsDeleted,
		u.Id,
		u.FirstName,
		u.LastName
		from dbo.Comment c inner join [dbo].[User] u on c.UserId=u.Id
		where c.Id=@id
end