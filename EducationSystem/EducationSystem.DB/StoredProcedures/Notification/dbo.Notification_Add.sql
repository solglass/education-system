CREATE PROCEDURE [dbo].[Notification_Add]
	@userId int,
    @authorId int,
	@message nvarchar(1000),
    @date datetime2(7)
AS
begin
	INSERT INTO [dbo].[Notification]
           ([UserId]
           ,[AuthorId]
           ,[Message]
           ,[Date]
           ,[IsRead])
     VALUES
           (@userId, 
           @authorId,
           @message,
           @date,
           0)
    select SCOPE_IDENTITY()
end
