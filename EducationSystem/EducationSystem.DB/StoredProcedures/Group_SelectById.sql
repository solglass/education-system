create proc dbo.Group_SelectById (
	@id int
) as
begin
	select
		Id,
		CourseID,
		StatusId,
		StartDate
	from dbo.[Group]
	where Id = @id
end

