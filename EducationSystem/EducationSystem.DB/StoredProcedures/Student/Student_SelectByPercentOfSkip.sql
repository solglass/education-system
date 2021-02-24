CREATE proc [dbo].[Student_SelectByPercentOfSkip](
@percent int
)
as
begin
	select
		u.Id,
		u.FirstName,
		u.LastName,
		u.Login,
		p.PercentOfSkip
	from
		(
		select
			a.UserId,
			cast (sum(case when a.IsAbsent=1 then 1.0 else 0 end)/ count(*)*100 as decimal(4,1)) as PercentOfSkip
		from dbo.[Attendance] a 
		group By a.UserId) as p
	inner join dbo.[User] u on p.UserId=u.Id
	where PercentOfSkip >= @percent
end