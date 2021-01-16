create proc dbo.Group_Update (
	@id int,
	@courseId int,
	@statusId int,
	@startDate date
) as
begin
	update dbo.[Group]
	set
		CourseID = @courseId,
		StatusId = @statusId,
		StartDate = @startDate
	where Id = @id
end