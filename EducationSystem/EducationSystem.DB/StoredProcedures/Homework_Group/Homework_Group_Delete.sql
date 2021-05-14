 CREATE proc [dbo].[Homework_Group_Delete] (
@HomeworkId int,
@GroupId int
) as
begin
	delete from dbo.Homework_Group
	where HomeworkId = @HomeworkId and GroupId = @GroupId 
end