CREATE proc [dbo].[HomeworkAttempt_SelectByUserId]
(@id int)
as
begin
	select
		hwa.Id,
		hwa.Comment,
		hwa.HomeworkId,
		hwa.StatusId,
		hwas.Id,
		hwas.Name,
		hw.Id,
		hw.Description,
		u.Id,
		u.FirstName,
		u.LastName,
		u.UserPic,
		a.Id,
		a.Path,
		c.Id,
		c.Message
	from dbo.HomeworkAttempt hwa
		inner join dbo.[User] u on hwa.UserId = u.Id
		inner join dbo.Homework hw on hwa.HomeworkId = hw.Id
		inner join dbo.HomeworkAttemptStatus hwas on hwa.StatusId = hwas.Id
		left join dbo.HomeworkAttempt_Attachment hwaa on hwaa.HomeworkAttemptID = hwa.Id
		left join dbo.Attachment a on hwaa.AttachmentID = a.Id
		left join dbo.Comment c on hwa.Id = c.HomeworkAttemptId
	where hwa.IsDeleted = 0 and u.id = @id
end
