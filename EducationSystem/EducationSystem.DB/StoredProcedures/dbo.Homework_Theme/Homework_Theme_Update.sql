CREATE proc [dbo].[Homework_Theme_Update] (
@id int,
@ThemeId int
) as
begin 
	update dbo.Homework_Theme
	set
		ThemeId = @ThemeId
	where Id = @id
end
