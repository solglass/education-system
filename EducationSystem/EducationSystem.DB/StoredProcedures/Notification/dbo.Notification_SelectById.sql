CREATE PROCEDURE [dbo].[Notification_SelectById]
	@Id int
AS
begin
	SELECT
		N.Id,
		N.UserId,
		N.AuthorId,
		N.Message,
		N.Date,
		N.IsRead,
		U.Id,
		U.FirstName,
		U.LastName,
		U.UserPic,
		A.Id,
		A.FirstName,
		A.LastName,
		A.UserPic
	from [dbo].[Notification] as N
	inner join [dbo].[User] as U on U.Id = N.UserId
	inner join [dbo].[User] as A on A.Id = N.AuthorId
	where N.Id=@Id
end
