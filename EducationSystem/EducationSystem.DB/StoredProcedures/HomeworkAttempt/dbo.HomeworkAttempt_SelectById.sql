CREATE proc [dbo].[HomeworkAttempt_SelectById] (
@id int
)
as
begin
	select
		hma.Id,
		hma.IsDeleted,
		hma.Comment,
		hma.HomeworkID,
		hma.StatusID,
		u.Id,
		u.FirstName,
		u.LastName,
		u.UserPic,
		hm.Id,
		hm.Description,
		hmas.Id
	from dbo.HomeworkAttempt hma 
		inner join dbo.[User] u on hma.UserID = u.Id
		inner join dbo.Homework hm  on hma.HomeworkID = hm.Id
		inner join dbo.HomeworkAttemptStatus hmas on hma.StatusID = hmas.Id
		left join dbo.HomeworkAttempt_Attachment hwaa on hwaa.HomeworkAttemptID = hma.Id
		left join dbo.Attachment a on hwaa.AttachmentID = a.Id
		left join dbo.Comment c on hma.Id = c.HomeworkAttemptId
	
	where hma.Id = @id
end