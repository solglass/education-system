create proc [dbo].[Homework_Update] (
@id int,
@description nvarchar(Max),
@startDate datetime2,
@deadlineDate datetime2,
@groupId int,
@isOptional bit
)
as
begin
	update dbo.Homework
	set
		description = @description,
		startDate  = @startDate,
		deadlineDate = @deadlineDate,
		groupId = @groupId,
		isOptional  = @isOptional 
	where Id = @id
end