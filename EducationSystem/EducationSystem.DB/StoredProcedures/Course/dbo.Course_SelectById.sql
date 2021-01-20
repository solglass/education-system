CREATE proc [dbo].[Course_SelectById] (
@id int
)
as
begin 
	select
		Id,
		Name,
		Description,
		Duration,
		IsDeleted
		from dbo.Course
		where Id=@id
end