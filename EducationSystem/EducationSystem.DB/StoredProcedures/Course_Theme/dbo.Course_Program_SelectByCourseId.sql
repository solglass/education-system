CREATE proc [dbo].[Course_Program_SelectByCourseId] (
@id int
)
as
begin 
	select
		ct.Id,
		ct.[Order],
		c.Id,
		c.[Name],
		t.Id,
		t.[Name]
	from  dbo.Course_Theme ct
		join  dbo.Course c on c.Id = ct.CourseID
		join dbo.Theme t on t.Id = ct.ThemeID	
	where c.Id = @id and t.IsDeleted=0
	order by ct.[Order] 
end