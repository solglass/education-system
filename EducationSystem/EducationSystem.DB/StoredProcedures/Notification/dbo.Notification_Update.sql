CREATE PROCEDURE [dbo].[Notification_Update]
	@Id int,
	@message nvarchar(1000),
	@date datetime2
AS
begin
	UPDATE [dbo].[Notification]
	SET [Message] = @message,
		[Date] = @date
	WHERE Id=@id
end
