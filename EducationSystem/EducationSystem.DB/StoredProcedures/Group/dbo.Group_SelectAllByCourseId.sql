create proc dbo.Group_SelectAllByCourseId
(@courseId int) as
begin
	select 
		g.Id,
		g.CourseID,
		g.StartDate,
		g.StatusId as Id
	from dbo.[Group] g 
	where g.CourseID=@courseId
end