create proc [dbo].[Theme_Tag_Delete] (
@id int
) as
begin
	delete from dbo.Theme_Tag
	where Id = @id
end