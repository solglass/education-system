create proc [dbo].[Theme_SelectAllByHomeworkId](
@homeworkId int) as
begin
	select
		t.Id,
		t.Name
	from dbo.Theme t 
	inner join dbo.Homework_Theme ht on ht.HomeworkId = @homeworkId
	inner join dbo.Homework h on h.Id = ht.HomeworkId
	where h.IsDeleted = 0
end 