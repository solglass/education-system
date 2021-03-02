CREATE  proc [dbo].[HomeworkAttempt_SelectByHomeworkId] (
	@homeworkId int
)
as
begin
	select 
		hma.Id,
		hma.Comment,
		hma.HomeworkID,
		hma.StatusID,
		hma.IsDeleted,
		u.Id,
		u.FirstName,
		u.LastName,
		u.UserPic,
		hmas.Id,
		hmas.Id as homeworkAttemptStatus,
		hmas.Name
	from dbo.HomeworkAttempt hma 
		inner join dbo.[User] u on hma.UserID = u.Id
		inner join dbo.HomeworkAttemptStatus hmas on hma.StatusID = hmas.Id

	where hma.HomeworkId = @homeworkId
end