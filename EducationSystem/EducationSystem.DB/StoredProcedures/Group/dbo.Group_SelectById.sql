create proc [dbo].[Group_SelectById] (
	@id int
) as
begin
	select
		g.Id,
		g.StartDate,
		c.Id,
		c.Name,
		c.Description,
		c.Duration,
		g.StatusId as Id,
		s.Id,
		s.FirstName,
		s.LastName,
		s.[Login],
		s.UserPic,
		t.Id,
		t.FirstName,
		t.LastName,
		t.[Login],
		t.UserPic,
		tu.Id,
		tu.FirstName,
		tu.LastName,
		tu.[Login],
		tu.UserPic
	from dbo.[Group] g 
	inner join dbo.Course c on g.CourseID = c.Id
	left join dbo.Student_Group sg on g.Id = sg.GroupId
	left join dbo.[User] s on s.Id = sg.UserId
	left join dbo.Teacher_Group tg on g.Id = tg.GroupID
	left join dbo.[User] t on t.Id = tg.UserId
	left join dbo.Tutor_Group tug on g.Id = tug.GroupID
	left join dbo.[User] tu on tu.Id = tug.UserId
	where g.Id = @id
end

