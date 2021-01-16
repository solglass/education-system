CREATE proc [dbo].[HomeworkAttempt_SelectAll]
as
begin
	select 
	hma.Id,
	hma.Comment,
	u.Id,
	u.FirstName + ' ' + u.LastName as UserName,
	hma.HomeworkID,
	hm.Description,
	hma.StatusID,
	hmas.Name
	from dbo.HomeworkAttempt hma inner join dbo.[User] u on hma.UserID = u.Id
	inner join dbo.Homework hm  on hma.HomeworkID = hm.Id
	inner join dbo.HomeworkAttemptStatus hmas on hma.StatusID = hmas.Id
end
