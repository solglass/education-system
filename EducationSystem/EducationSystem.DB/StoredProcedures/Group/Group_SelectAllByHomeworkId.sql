create proc dbo.Group_SelectAllByHomeworkId
(@HomeworkId int) as
begin
	select 
		g.Id,
		g.CourseID,
		g.StartDate,
		g.StatusId
	from dbo.[Group] g 
	inner join Homework_Group hg on g.Id = hg.GroupId
	where hg.HomeworkId = @HomeworkId
end

