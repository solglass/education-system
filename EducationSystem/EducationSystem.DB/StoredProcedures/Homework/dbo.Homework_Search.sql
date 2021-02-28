CREATE PROCEDURE [dbo].[Homework_Search]
@groupId int = null,
@themeId int = null,
@tagId int = null
as
begin
SELECT h.[Id]
      ,h.[Description]
      ,h.[StartDate]
      ,h.[DeadlineDate]
      ,h.[IsOptional]
      ,g.Id
      ,tg.Id
      ,tg.[Name]
      ,th.Id
      ,th.[Name]
  FROM dbo.Homework h
  left join dbo.Homework_Theme hth on h.id = hth.HomeworkId
  left join dbo.Theme th on hth.ThemeId = th.id
  left join dbo.Homework_Tag htg on h.id = htg.HomeworkId
  left join dbo.Tag tg on htg.TagId = tg.id
    inner join dbo.[Group] g on h.GroupID = g.id
  where h.IsDeleted = 0 and 
  (@groupId is not null and g.Id = @groupId or @groupId is null) and
  (@themeId is not null and th.Id = @themeId or @themeId is null) and
  (@tagId is not null and tg.Id = @tagId or @tagId is null)
  
end
