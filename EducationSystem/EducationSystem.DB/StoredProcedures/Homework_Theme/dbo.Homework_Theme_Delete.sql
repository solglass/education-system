CREATE  proc [dbo].[Homework_Theme_Delete] (
@homeworkId int, @themeId int)
as
begin 
	delete from dbo.Homework_Theme
	where HomeworkId=@homeworkId and ThemeId=@themeId
end
