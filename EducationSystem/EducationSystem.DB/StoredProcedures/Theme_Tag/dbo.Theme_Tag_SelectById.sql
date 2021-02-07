create proc [dbo].[Theme_Tag_SelectById](
	@id int
) as
begin 
select
		tt.Id,
		tt.TagId,
		t.Name,
		tt.ThemeId
		from dbo.Theme_Tag as tt inner join dbo.Tag as t on tt.Id = t.Id where tt.Id=@id
end