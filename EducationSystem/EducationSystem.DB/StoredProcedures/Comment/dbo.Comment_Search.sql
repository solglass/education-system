Create proc [dbo].[Comment_Search]
@homeworkAttemptId int = null,
@homeworkId int = null
as
begin
	select c.Id,
		c.Message,
		u.Id,
		u.FirstName,
		u.LastName
		from dbo.Comment c 
		left join [dbo].[User] u on c.UserId=u.Id
		left join [dbo].[HomeworkAttempt] ha on c.HomeworkAttemptId = ha.Id
		where c.IsDeleted = 0 and
		(@homeworkAttemptId is not null and ha.Id = @homeworkAttemptId or @homeworkAttemptId is null) and 
		(@homeworkId is not null and ha.HomeworkId = homeworkId or homeworkId is null)
end