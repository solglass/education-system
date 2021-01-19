CREATE proc [dbo].[Course_Theme_SelectAll]
as
begin
	select
		Id,
		CourseID,
		ThemeID
		from dbo.Course_Theme
end