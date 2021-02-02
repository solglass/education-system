CREATE proc [dbo].[Homework_SelectById] (
	@id int
) as
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
      ,ha.Id
      ,ha.Comment
      ,ha.IsDeleted
      ,has.Id
      ,has.[Name]
      ,u.Id
      ,u.[FirstName]
      ,u.[LastName]
      ,u.[Login]
      ,u.[UserPic]
      ,th.Id
      ,th.[Name]
  FROM dbo.Homework h
  left join dbo.Homework_Theme hth on h.id = hth.HomeworkId
  left join dbo.Theme th on hth.ThemeId = th.id
  left join dbo.Homework_Tag htg on h.id = htg.HomeworkId
  left join dbo.Tag tg on htg.ThemeId = tg.id
  left join dbo.HomeworkAttempt ha on h.Id = ha.HomeworkId
  inner join dbo.HomeworkAttemptStatus has on has.Id = ha.StatusId
  inner join dbo.[User] u on u.Id = ha.UserId
  inner join dbo.[Group] g on h.GroupID = g.id

  
  where h.IsDeleted = 0 and h.Id = @id

end