create proc dbo.Group_Add (
	@courseId int,
	@statusId int,
	@startDate date
) as
begin
	insert into dbo.[Group] (CourseID, StatusId, StartDate)
	values (@courseId, @statusId, @startDate)
	select SCOPE_IDENTITY()
end