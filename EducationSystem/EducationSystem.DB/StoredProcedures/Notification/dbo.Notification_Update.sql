CREATE PROCEDURE [dbo].[Notification_Update]
	@Id int,
	@message nvarchar(1000)
AS
begin
	UPDATE [dbo].[Notification]
	SET [Message] = @message      
	WHERE Id=@id
end
