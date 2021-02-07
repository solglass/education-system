create proc [dbo].[Course_Theme_SelectAllByThemeId]
(@id int) as
begin
	select *
	from dbo.Course_Theme
	where ThemeID=@id
end 