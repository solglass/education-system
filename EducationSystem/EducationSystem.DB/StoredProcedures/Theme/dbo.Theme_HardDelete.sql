CREATE proc [dbo].[Theme_HardDelete]
(@id int)
as
begin
DELETE FROM dbo.Theme WHERE Id = @id
end
