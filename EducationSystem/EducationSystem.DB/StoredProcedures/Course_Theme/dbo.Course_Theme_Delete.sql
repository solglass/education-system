CREATE proc [dbo].[Course_Theme_Delete](
@id int
)as
begin
	delete from dbo.Course_Theme
	where Id=@id
end