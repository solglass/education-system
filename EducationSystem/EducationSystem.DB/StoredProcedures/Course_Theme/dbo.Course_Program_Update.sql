CREATE proc [dbo].[Course_Program_Update](
@courseId int, 
@themeId int,
@order int
) as
begin
	insert into dbo.Course_Theme(CourseID, ThemeID, [Order])
	values(@courseId, @themeId, @order)
end
