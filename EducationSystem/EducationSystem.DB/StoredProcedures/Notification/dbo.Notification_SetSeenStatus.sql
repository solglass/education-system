CREATE PROCEDURE [dbo].[Notification_SetSeenStatus]
	@Id int,
	@isRead bit
AS
begin
	UPDATE [dbo].[Notification]
	SET [IsRead] = @isRead     
	WHERE Id=@id
end

