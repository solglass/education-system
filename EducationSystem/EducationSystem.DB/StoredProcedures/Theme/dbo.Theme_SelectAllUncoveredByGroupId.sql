create proc [dbo].[Theme_SelectAllUncoveredByGroupId]
(@groupId int) as
begin
	select
		g.Id,
		g.CourseID,
		g.StartDate,
		t.Id,
		t.Name
	from dbo.[Group] g 
		inner join dbo.[Course_Theme] ct on ct.CourseID=g.CourseID
		inner join dbo.[Theme] t on t.Id=ct.ThemeID 
	WHERE g.Id=@groupId and t.Id NOT IN (SELECT t.Id 
							from [dbo].[Lesson] l 
								inner join dbo.Lesson_Theme lt on lt.LessonID=l.Id
								inner join dbo.Theme t on t.Id=lt.ThemeID
							where l.GroupID=@groupId and l.Date<(SELECT SYSDATETIME())) and t.IsDeleted=0  
			 
end