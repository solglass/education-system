CREATE proc [dbo].[Course_Add] (
	@Name nvarchar(50),
	@Description nvarchar(MAX),
	@Duration int
) as
begin
	insert into dbo.Course (Name, Description, Duration, IsDeleted)
	values (@Name, @Description, @Duration, 0)
end