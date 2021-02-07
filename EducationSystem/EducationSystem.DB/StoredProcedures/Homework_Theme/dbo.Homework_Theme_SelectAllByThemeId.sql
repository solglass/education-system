create proc [dbo].[Homework_Theme_SelectAllByThemeId]
(@id int) as
begin
	select *
	from dbo.Homework_Theme
	where ThemeID=@id
end 