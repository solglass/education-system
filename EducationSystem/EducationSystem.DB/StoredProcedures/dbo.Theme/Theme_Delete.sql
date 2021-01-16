CREATE proc [dbo].[Theme_Delete]
(@id int)
as
begin
DELETE FROM dbo.Theme WHERE Id = @id
end
