create proc [dbo].[Homework_Add] (
	@description nvarchar(max),
	@startDate datetime2(7),
	@deadlineDate datetime2(7),
	@courseId int,
	@isOptional bit
 ) as
 begin
	insert into dbo.[Homework] ([Description]
           ,[StartDate]
           ,[DeadlineDate]
           ,[CourseID]
           ,[IsOptional]
		   ,[IsDeleted])
	values (@description, @startDate, @deadlineDate, @courseId, @isOptional, 0)
	select	SCOPE_IDENTITY()
 end

