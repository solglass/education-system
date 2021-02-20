create proc [dbo].[Theme_Tag_Add] (
	@TagId int,
	@ThemeId int
) as
begin
	insert into dbo.Theme_Tag(TagId,ThemeId)
	values (@TagId,@ThemeId)
	select SCOPE_IDENTITY()
end