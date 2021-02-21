CREATE proc [dbo].[Course_Recover] (
	@id int)
as
begin
	update dbo.Course
	set
		IsDeleted = 0
	where Id = @id
end
