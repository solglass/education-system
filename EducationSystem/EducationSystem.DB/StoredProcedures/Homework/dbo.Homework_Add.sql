create proc [dbo].[Homework_Add] (
	@description nvarchar(max),
	@startDate datetime2(7),
	@deadlineDate datetime2(7),
	@groupId int,
	@isOptional bit


 ) as
 begin
	insert into dbo.[Homework] ([Description]
           ,[StartDate]
           ,[DeadlineDate]
           ,[GroupID]
           ,[IsOptional])
	values (@description, @startDate, @deadlineDate, @groupId, @isOptional)
	select SCOPE_IDENTITY()
 end

