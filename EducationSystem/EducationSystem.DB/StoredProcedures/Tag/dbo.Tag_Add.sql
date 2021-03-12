create proc [dbo].[Tag_Add] (
	@name nvarchar(30)
) as
begin
	insert into dbo.Tag (Name)
	values (@name)
	select SCOPE_IDENTITY()
end
