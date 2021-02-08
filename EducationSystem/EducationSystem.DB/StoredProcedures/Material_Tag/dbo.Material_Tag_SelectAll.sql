create proc [dbo].[Material_Tag_SelectAll]
as
begin 
select
		mt.Id,
		mt.TagId,
		t.Name,
		mt.MaterialId
		from dbo.Material_Tag as mt inner join dbo.Tag as t on mt.Id = t.Id
end