CREATE proc [dbo].[Homework_SelectById] (
	@id int
) as
begin
 SELECT h.[Id]
      ,h.[Description]
      ,h.[StartDate]
      ,h.[DeadlineDate]
      ,h.[IsOptional]
	  ,h.IsDeleted
      ,c.id
      ,tg.Id
      ,tg.[Name]
      ,th.Id
      ,th.[Name]
  FROM dbo.Homework h
  left join dbo.Homework_Theme hth on h.id = hth.HomeworkId
  left join dbo.Theme th on hth.ThemeId = th.id

  left join dbo.Homework_Tag htg on h.id = htg.HomeworkId
  left join dbo.Tag tg on htg.TagId = tg.id

  inner join dbo.[Course] c on h.CourseID = c.id

  
  where h.Id = @id and (th.Id is null or th.IsDeleted = 0)

end