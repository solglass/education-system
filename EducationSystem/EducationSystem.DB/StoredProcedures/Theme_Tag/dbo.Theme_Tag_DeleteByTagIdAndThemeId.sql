create proc [dbo].[Theme_Tag_DeleteByTagIdAndThemeId] (
@themeId int, @tagId int
) as
begin
	delete from dbo.Theme_Tag
	where TagId=@tagId and ThemeId=@themeId
end