CREATE PROCEDURE [dbo].[Homework_Group_Add](
	@GroupId int,
	@HomeworkId int)
as
begin
Insert Into dbo.Homework_Group(GroupId,HomeworkId) Values (@GroupId,@HomeworkId)
select SCOPE_IDENTITY()
end