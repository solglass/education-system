create proc [dbo].[User_Change_Password]
(
@id int,
@oldPassword nvarchar(30),
@newPassword nvarchar(30)
)
as

begin
    update dbo.[User]
        set
            Password = @newPassword
            where Id = @id and Password = @oldPassword
end