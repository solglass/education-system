CREATE proc [dbo].[Homework_Theme_SelectById] (
@Id int 
) as
begin 
	select
		Id,
		HomeworkId,
		ThemeId
		from dbo.Homework_Theme
		where Id=@id
end
