CREATE PROCEDURE [dbo].[Homework_SelectAll]
as
begin
SELECT [Id]
      ,[Description]
      ,[StartDate]
      ,[DeadlineDate]
      ,[GroupID]
      ,[IsOptional]
      ,[IsDeleted]
  FROM [dbo].[Homework]
end
