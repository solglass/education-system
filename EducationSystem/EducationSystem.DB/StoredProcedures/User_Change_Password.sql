create proc [dbo].[User_Change_Password]
(
@oldPassword nvarchar(30),
@newPassword nvarchar(30)
)
as

begin
    update dbo.[User]
        set
            Password = @newPassword
            where Password = @oldPassword
end