CREATE proc [dbo].[Student_SelectByPercentOfSkip](
@percent int,
@groupId int
)
as
begin
	select
		
		u.Id,
		u.FirstName,
		u.LastName,
		u.Login,
		u.UserPic,
		p.PercentOfSkipLessons
		
	from
			(
			select
				a.UserId,
				cast (sum(case when a.IsAbsent=1 then 1.0 else 0 end)/ count(*)*100 as decimal(4,1)) as PercentOfSkipLessons
			from dbo.[Attendance] a 
			group By a.UserId
			) as p
		inner join dbo.[User] u on p.UserId=u.Id
		inner join dbo.[Student_Group] sg on u.Id=sg.UserId
		inner join dbo.[Group] g on sg.GroupId = g.Id
	where PercentOfSkipLessons >= @percent and u.IsDeleted = 0 and g.StatusId = 3 and g.Id = @groupId
end