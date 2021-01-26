CREATE PROCEDURE [dbo].[Homework_SelectAll]
as
begin
SELECT h.[Id]
      ,h.[Description]
      ,h.GroupID
      ,h.[StartDate]
      ,h.[DeadlineDate]
      ,h.[IsOptional]
      ,h.[IsDeleted]
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
      ,u.[BirthDate]
      ,u.[Login]
      ,u.[Password]
      ,u.[Phone]
      ,u.[UserPic]
      ,u.[Email]
      ,u.[RegistrationDate]
      ,u.[IsDeleted]
      ,th.Id
      ,th.[Name]
  FROM dbo.Homework h
  left join dbo.Homework_Theme hth on h.id = hth.HomeworkId
  left join dbo.Theme th on hth.ThemeId = th.id
  left join dbo.Homework_Tag htg on h.id = htg.HomeworkId
  left join dbo.Tag tg on htg.ThemeId = tg.id
  left join dbo.HomeworkAttempt ha on h.Id = ha.HomeworkId
  left join dbo.HomeworkAttemptStatus has on has.Id = ha.StatusId
  left join dbo.[User] u on u.Id = ha.UserId

  
  where h.IsDeleted = 0
  
end
