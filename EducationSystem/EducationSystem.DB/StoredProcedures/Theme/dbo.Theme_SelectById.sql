CREATE proc [dbo].[Theme_SelectById]
(@id int)
as
begin
select
	t.Id,
	t.Name
from dbo.Theme t 
	where t.Id=@id
end