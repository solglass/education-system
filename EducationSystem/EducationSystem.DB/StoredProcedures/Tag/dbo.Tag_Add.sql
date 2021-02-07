create proc [dbo].[Tag_Add] (
	@Name nvarchar(30)
) as
begin
	insert into dbo.Tag (Name)
	values (@Name)
	select SCOPE_IDENTITY()
end
