CREATE proc [dbo].[Course_Add] (
	@Name nvarchar(50),
	@Description nvarchar(MAX),
	@Duration int
) as
begin
	insert into dbo.Course (Name, Description, Duration)
	values (@Name, @Description, @Duration)
end