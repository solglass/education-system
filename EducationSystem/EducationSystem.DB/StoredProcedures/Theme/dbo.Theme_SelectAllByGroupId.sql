create proc [dbo].[Theme_SelectAllByGroupId](
@groupId int) as
begin
	select
		g.Id,
		g.CourseID,
		t.Id,
		t.Name
	from dbo.[Group] g 
	inner join dbo.[Course_Theme] ct on ct.CourseID=g.CourseID
	left join dbo.[Theme] t on t.Id=ct.ThemeID
	where g.Id=@groupId
end