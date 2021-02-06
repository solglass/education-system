create proc [dbo].[Theme_Tag_SelectAll]
as
begin 
	select
		tt.Id,
		tt.TagId,
		t.Name,
		tt.ThemeId
		from dbo.Theme_Tag tt inner join dbo.Tag t on tt.Id = t.Id
end