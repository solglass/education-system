create PROCEDURE [dbo].Homework_SelectByGroupId
@groupId int
as
begin
SELECT h.[Id]
      ,h.[Description]
      ,h.[StartDate]
      ,h.[DeadlineDate]
      ,h.[IsOptional]
      ,c.Id
      ,g.Id
      ,g.CourseID
      ,g.StartDate
      ,g.StatusId as Id
  FROM dbo.Homework h
  left join dbo.Homework_Group hg on h.id = hg.HomeworkId
  left join dbo.[Group] g on hg.GroupId = g.Id

  inner join dbo.[Course] c on h.CourseID = c.id
  
  where h.IsDeleted = 0 and g.Id = @groupId
  
end
