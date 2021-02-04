create proc dbo.Group_SelectAllByCourseId
(@courseId int) as
begin
	select 
		g.Id,
		g.CourseID,
		g.StartDate,
		gs.Id,
		gs.Name
	from dbo.[Group] g inner join dbo.GroupStatus gs on gs.Id = g.StatusId
	where g.CourseID=@courseId
end