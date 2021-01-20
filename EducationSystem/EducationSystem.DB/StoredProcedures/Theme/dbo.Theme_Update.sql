CREATE proc [dbo].[Theme_Update]
(@id int, @name nvarchar(100))
as 
begin
update dbo.Theme
set 
Name = @name
	where Id = @id
end
