CREATE proc [dbo].[Course_Theme_SelectById] (
@id int
) as
begin
select
		Id,
		CourseID,
		ThemeID
		from dbo.Course_Theme
		where Id=@id
end
