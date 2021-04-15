CREATE proc [dbo].[Create_Report]
as
begin
with st as 
	(select  
	g.Id as GroupId,
	count(sg.UserId) as StudentCount
	from [dbo].[Student_Group] sg join [dbo].[Group] g on g.Id = sg.GroupId 
	join [dbo].[User] us on sg.UserID = us.Id 
	where us.IsDeleted = 0
	group by g.Id)
	,
	tt as
	(select  
	g.Id as GroupId,
	count(tg.UserId) as TutorCount
	from [dbo].[Tutor_Group] tg join [dbo].[Group] g on g.Id = tg.GroupId 
	join [dbo].[User] us on tg.UserID = us.Id 
	where us.IsDeleted = 0
	group by g.Id)
	,
	tc as
	(select  
	g.Id as GroupId,
	count(tc.UserId) as TeacherCount
	from [dbo].[Teacher_Group] tc join [dbo].[Group] g on g.Id = tc.GroupId 
	join [dbo].[User] us on tc.UserID = us.Id 
	where us.IsDeleted = 0
	group by g.Id)
	,
	gd as 
	(select gr.StartDate as StartDate,
	gr.Id as GroupId,
	cr.Name,
	DATEADD(week, cr.Duration, gr.StartDate) as EndDate
	from [dbo].[Group] gr  join [dbo].[Course] cr 
	on gr.CourseID = cr.Id 
	where cr.IsDeleted = 0)
	select gd.GroupId,
	Name,
	StartDate,
	EndDate,
	isnull(st.StudentCount,0) as StudentCount,
	isnull(tt.TutorCount,0) as TutorCount,
	isnull(tc.TeacherCount,0) as TeacherCount

	from gd left join st on gd.GroupId = st.GroupId
	left join tt on gd.GroupId = tt.GroupId
	left join tc on gd.GroupId = tc.GroupId
	where StudentCount >0
end