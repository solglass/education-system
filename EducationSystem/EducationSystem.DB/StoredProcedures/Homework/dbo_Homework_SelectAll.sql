CREATE PROCEDURE [dbo].[Homework_SelectAll]
as
begin
SELECT h.[Id]
      ,h.[Description]
      ,h.[StartDate]
      ,h.[DeadlineDate]
      ,h.[IsOptional]
      ,h.[IsDeleted]
      ,g.Id
      ,tg.Id
      ,tg.[Name]
      ,th.Id
      ,th.[Name]
  FROM dbo.Homework h
  left join dbo.Homework_Theme hth on h.id = hth.HomeworkId
  left join dbo.Theme th on hth.ThemeId = th.id
  left join dbo.Homework_Tag htg on h.id = htg.HomeworkId
  left join dbo.Tag tg on htg.ThemeId = tg.id
    inner join dbo.[Group] g on h.GroupID = g.id
  where h.IsDeleted = 0
  
end
