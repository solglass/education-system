CREATE proc [dbo].[Homework_Theme_SelectAll]
as
begin 
	select
		Id,
		HomeworkId,
		ThemeId
		from dbo.Homework_Theme
end
