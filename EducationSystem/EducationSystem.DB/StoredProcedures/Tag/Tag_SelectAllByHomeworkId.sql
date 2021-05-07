create proc [dbo].[Tag_SelectAllByHomeworkId](
@homeworkId int) as
begin
	select
		t.Id,
		t.Name
	from dbo.Tag t 
	inner join dbo.Homework_Tag ht on ht.HomeworkId = @homeworkId
	inner join dbo.Homework h on h.Id = ht.HomeworkId
	where h.IsDeleted = 0
end 