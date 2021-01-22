CREATE proc [dbo].[Homework_SelectById] (
	@id int
) as
begin
	select
		[Id]
      ,[Description]
      ,[StartDate]
      ,[DeadlineDate]
      ,[GroupID]
      ,[IsOptional]
      ,[IsDeleted]
	from dbo.[Homework]
	where Id = @id
end