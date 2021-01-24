CREATE proc [dbo].[Course_Add] (
	@name nvarchar(50),
	@description nvarchar(MAX),
	@duration int
) as
begin
	insert into dbo.Course (Name, Description, Duration, IsDeleted)
	values (@name, @description, @duration, 0)
	select SCOPE_IDENTITY()
end