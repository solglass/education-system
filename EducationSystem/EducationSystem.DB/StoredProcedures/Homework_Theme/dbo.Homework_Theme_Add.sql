create proc [dbo].[Homework_Theme_Add](
@homeworkId int, @themeId int) as
begin
	insert into dbo.Homework_Theme(HomeworkId, ThemeId)
	values(@homeworkId, @themeId)
	select SCOPE_IDENTITY()
end