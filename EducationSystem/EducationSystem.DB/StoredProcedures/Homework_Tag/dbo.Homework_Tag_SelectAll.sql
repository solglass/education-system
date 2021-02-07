create proc [dbo].[Homework_Tag_SelectAll]
as
begin 

			select
		ht.Id,
		ht.TagId,
		t.Name,
		ht.HomeworkId
		from dbo.Homework_Tag as ht inner join dbo.Tag as t on ht.Id = t.Id
end