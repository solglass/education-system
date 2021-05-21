CREATE PROCEDURE [dbo].[Homework_Group_SelectByHomeworkId](
	@HomeworkId int)
as
begin
select hg.HomeworkId, hg.GroupId from Homework_Group hg 
where hg.HomeworkId = @HomeworkId
end