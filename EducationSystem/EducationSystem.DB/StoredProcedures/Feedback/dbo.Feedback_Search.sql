Create proc [dbo].[Feedback_Search]
@lessonId int = null,
@groupId int = null, 
@courseId int = null
as
select
	f.Id,
	f.UserId,
	f.Message,
	f.LessonID,
	l.Id,
	l.Date,
	ul.Id, 
	u.Id,
	u.FirstName,
	u.LastName,
	u.Phone,
	u.Email,
	u.UserPic
	from dbo.Feedback f inner join dbo.UnderstandingLevel ul on f.UnderstandingLevelId=ul.Id 
	inner join dbo.[User] u on u.Id = f.UserID 
	inner join dbo.Lesson l on l.Id = f.LessonID
	inner join dbo.[Group] g on l.GroupID = g.Id
	where (@lessonId is not null and f.LessonID = @lessonId or @lessonId is null) and
	(@groupId is not null and l.GroupID = @groupId or @groupId is null) and
	(@courseId is not null and g.CourseID = @courseId or @courseId is null)