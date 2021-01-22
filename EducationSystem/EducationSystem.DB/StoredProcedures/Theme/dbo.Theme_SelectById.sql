CREATE proc [dbo].[Theme_SelectById]
(@id int)
as
begin
select Id,Name
from dbo.Theme
where Id=@id

end
