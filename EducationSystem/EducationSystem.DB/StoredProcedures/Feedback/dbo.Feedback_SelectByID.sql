create proc [dbo].[Feedback_SelectByID](@id int)
as
begin
select
	f.Id,
	f.UserId,
	f.Message,
	f.LessonID,
	l.Id,
	l.Date,
	ul.Id, 
	ul.Id as understendinglevel,
	u.Id,
	u.FirstName,
	u.LastName,
	u.Phone,
	u.Email,
	u.UserPic
	from dbo.Feedback f inner join dbo.UnderstandingLevel ul on f.UnderstandingLevelId=ul.Id inner join dbo.[User] u on u.Id = f.UserID inner join dbo.Lesson l on l.Id = f.LessonID
	where f.Id = @id
end