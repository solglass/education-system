CREATE proc [dbo].[HomeworkAttempt_SelectByHomeworkId] (
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
		hmas.Name
	from dbo.HomeworkAttempt hma 
		inner join dbo.[User] u on hma.UserID = u.Id
		inner join dbo.HomeworkAttemptStatus hmas on hma.StatusID = hmas.Id
		left join dbo.HomeworkAttempt_Attachment hwaa on hwaa.HomeworkAttemptID = hma.Id
		left join dbo.Attachment a on hwaa.AttachmentID = a.Id
	
	where hma.HomeworkId = @homeworkId
end