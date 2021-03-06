create proc [dbo].[User_Add]
(
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@birthDate datetime2,
	@login nvarchar(50),
	@password nvarchar(100),
	@phone nvarchar(20),
	@userPic nvarchar(1000),
	@email nvarchar(60)
)
as
begin
	INSERT INTO	dbo.[User] (FirstName, LastName, BirthDate, Login, Password, Phone, UserPic, Email, IsDeleted) 
	VALUES(@firstName, @lastName, @birthDate, @login, @password, @phone, @userPic, @email, 0)
	select SCOPE_IDENTITY()
end
