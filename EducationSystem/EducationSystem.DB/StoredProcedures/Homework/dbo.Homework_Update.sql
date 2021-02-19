create proc [dbo].[Homework_Update] (
@id int,
@description nvarchar(Max),
@startDate datetime2,
@deadlineDate datetime2,
@isOptional bit
)
as
begin
	update dbo.Homework
	set
		description = @description,
		startDate  = @startDate,
		deadlineDate = @deadlineDate,
		isOptional  = @isOptional 
	where Id = @id
end