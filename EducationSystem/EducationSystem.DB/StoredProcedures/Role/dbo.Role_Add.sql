create proc [dbo].[Role_Add](
	@name nvarchar(60)
) as
begin
	insert into dbo.Role (Name) VALUES(@name)
	select SCOPE_IDENTITY()
end