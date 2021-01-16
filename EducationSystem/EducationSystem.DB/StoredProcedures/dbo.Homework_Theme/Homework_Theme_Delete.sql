CREATE  proc [dbo].[Homework_Theme_Delete] (
@Id int)
as
begin 
	delete from dbo.Homework_Theme
	where Id=@id
end
