create proc dbo.Group_SelectAllByCourseId
(@courseId int) as
begin
	select 
		g.Id,
		g.StartDate,
		g.StatusId as Id,
		c.Id,
		c.Name
		
	from dbo.[Group] g 
	inner join dbo.[Course] c on c.Id = g.CourseID
	where g.CourseID=@courseId
end