USE [DevEdu]
GO
/****** Object:  StoredProcedure [dbo].[Create_Report]    Script Date: 04.02.2021 19:27:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	(select Min(gr.StartDate) as StartDate,
	gr.Id as GroupId,
	cr.Name,
	Max(ls.Date) as EndDate
	from [dbo].[Group] gr join [dbo].[Course] cr 
	on gr.CourseID = cr.Id join [dbo].[Lesson] ls on ls.GroupID = gr.Id
	join st on gr.Id = st.GroupId
	where cr.IsDeleted = 0 and ls.IsDeleted = 0
	group by gr.Id,cr.Name)
	select gd.GroupId,
	Name,
	StartDate,
	EndDate,
	Max(st.StudentCount) as StudentCount,
	Max(tt.TutorCount) as TutorCount,
	Max(tc.TeacherCount) as TeacherCount

	from gd join st on gd.GroupId = st.GroupId
	join tt on gd.GroupId = tt.GroupId
	join tc on gd.GroupId = tc.GroupId
	group by  gd.GroupId,Name,StartDate,EndDate
end