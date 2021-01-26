CREATE proc [dbo].[Homework_SelectById] (
	@id int
) as
begin
    select
       h.[Id]
      ,h.[Description]
      ,h.[StartDate]
      ,h.[DeadlineDate]
      ,h.[IsOptional]
      ,h.[IsDeleted]
      ,g.Id
      ,g.StartDate
      ,gs.id
      ,gs.[Name]
      ,tg.Id
      ,tg.[Name]
      ,ha.Id
      ,ha.Comment
      ,ha.StatusId
      ,ha.UserId
      ,ha.IsDeleted
      ,th.Id
      ,th.[Name]
  FROM dbo.Homework h
  left join dbo.[Group] g on h.GroupID = g.Id
  left join dbo.[GroupStatus] gs on g.StatusId = gs.Id
  left join dbo.Homework_Theme hth on h.id = hth.HomeworkId
  left join dbo.Theme th on hth.ThemeId = th.id
  left join dbo.Homework_Tag htg on h.id = htg.HomeworkId
  left join dbo.Tag tg on htg.ThemeId = tg.id
  left join dbo.HomeworkAttempt ha on h.Id = ha.HomeworkId
  where h.IsDeleted = 0 and h.Id = @id
end