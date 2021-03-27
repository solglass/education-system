CREATE PROCEDURE [dbo].[Notification_ReadOrUnread]
	@Id int,
	@isRead bit
AS
begin
	UPDATE [dbo].[Notification]
	SET [IsRead] = @isRead     
	WHERE Id=@id
end

