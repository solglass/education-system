CREATE PROCEDURE [dbo].[Notification_Delete]
	@Id int
AS
begin
	DELETE FROM [dbo].[Notification]
      WHERE Id=@Id
end
