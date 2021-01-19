CREATE proc [dbo].[Course_SelectAll]
as
begin 
	select
		Id,
		Name,
		Description,
		Duration,
		IsDeleted
	from dbo.Course
	where IsDeleted=0
end