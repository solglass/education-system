CREATE proc [dbo].[Comment_SelectByHomeworkId] (
@homeworkId int)
as
begin
	select
		c.Id,
		c.Message,
		u.Id,
		u.FirstName,
		u.LastName,
		u.Login,
		u.UserPic,
		hwa.Id
	from dbo.Comment c
		inner join dbo.[User] u on c.UserID = u.Id
		inner join dbo.HomeworkAttempt hwa on c.HomeworkAttemptId = hwa.Id
		inner join dbo.Homework hw on hwa.HomeworkId = hw.Id
	where c.IsDeleted = 0 and hw.Id = @homeworkId
end
