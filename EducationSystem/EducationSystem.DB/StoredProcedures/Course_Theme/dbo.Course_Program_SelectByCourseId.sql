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
	from dbo.Course c 
		left join dbo.Course_Theme ct on c.Id = ct.CourseID
		left join dbo.Theme t on t.Id = ct.ThemeID	
	where c.Id = @id and (t.Id IS NULL or t.IsDeleted=0)
	order by ct.[Order] 
end