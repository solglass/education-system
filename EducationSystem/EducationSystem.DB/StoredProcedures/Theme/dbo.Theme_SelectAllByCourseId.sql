create proc dbo.Theme_SelectAllByCourseId
(@courseId int) as
begin
	select 
		t.Id,
		t.Name
	from dbo.Course_Theme ct inner join dbo.Theme t on ct.ThemeID= t.Id
	where ct.CourseID=@courseId and t.IsDeleted = 0
end