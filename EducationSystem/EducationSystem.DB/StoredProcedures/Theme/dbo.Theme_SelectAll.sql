CREATE proc [dbo].[Theme_SelectAll]
as
begin
select 
	t.Id,
	t.Name
from dbo.Theme t 
end