create proc [dbo].[PassedStudentsAttempt_SelectByGroupId]
(@groupId int) as
begin
 select
    u.Id,
    u.FirstName,
    u.LastName,
    u.[Login],
	r.Id,
	r.[Name]
	
    from dbo.HomeworkAttempt ha
    inner join [User] u on ha.UserId = u.Id
    inner join Student_Group sg on u.id = sg.UserID
    inner join [Group] g on sg.GroupID = g.Id
	inner join User_Role ur on u.Id = ur.UserID
	inner join [Role] r on ur.RoleID = r.Id
    where g.Id = @groupId and sg.UserId not in
	(
		select
		sg.UserId

		from dbo.HomeworkAttempt ha
		inner join [User] u on ha.UserId = u.Id
		inner join Student_Group sg on u.id = sg.UserID
		inner join [Group] g on sg.GroupID = g.Id
		where g.Id = @groupId and ha.StatusId !=3
	) 
end