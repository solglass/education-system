﻿CREATE proc [dbo].[Theme_SelectById]
(@id int)
as
begin
select 
	t.Id,
	t.Name,
	tg.Id,
	tg.Name
from dbo.Theme t 
	left join dbo.Theme_Tag tt on tt.ThemeId=t.Id
	left join dbo.Tag tg on tg.Id=tt.TagId
	where t.Id=@id and t.IsDeleted = 0

end