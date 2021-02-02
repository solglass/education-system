create proc [dbo].[Homework_Add] (
	@description nvarchar(max),
	@startDate datetime2(7),
	@deadlineDate datetime2(7),
	@groupId int,
	@isOptional bit,
	@isDeleted bit


 ) as
 begin
	insert into dbo.[Homework] ([Description]
           ,[StartDate]
           ,[DeadlineDate]
           ,[GroupID]
           ,[IsOptional]
           ,[IsDeleted])
	values (@description, @startDate, @deadlineDate, @groupId, @isOptional, @isDeleted)
 end