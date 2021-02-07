create proc [dbo].[User_Update]
(
	@id int, 
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@birthDate datetime2,
	@login nvarchar(50),
	@password nvarchar(30),
	@phone nvarchar(20),
	@userPic nvarchar(1000),
	@email nvarchar(60)
)
as
begin
	UPDATE dbo.[User] SET FirstName = @firstName, LastName = @lastName, BirthDate = @birthDate, Login = @login, Password = @password,
	Phone = @phone, UserPic = @userPic, Email = @email WHERE Id=@id
end