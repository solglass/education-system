CREATE proc [dbo].[Course_Delete] (
	@id int)
as
begin
	update dbo.Course
	set
		IsDeleted = 1
	where Id = @id
end
