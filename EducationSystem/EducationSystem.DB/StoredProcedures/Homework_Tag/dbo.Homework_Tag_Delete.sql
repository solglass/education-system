 CREATE proc [dbo].[Homework_Tag_Delete] (
@HomeworkId int,
@TagId int
) as
begin
	delete from dbo.Homework_Tag 
	where HomeworkId = @HomeworkId and TagId = @TagId 
end