create proc [dbo].[Theme_Tag_Delete]
@themeId int,
@tagId int
as
delete from dbo.Theme_Tag
where ThemeId = @themeId and TagId = @tagId
