create proc [dbo].[User_Role_DeleteAll]
	@userId int
as
	DELETE FROM dbo.User_Role WHERE UserID = @userId
