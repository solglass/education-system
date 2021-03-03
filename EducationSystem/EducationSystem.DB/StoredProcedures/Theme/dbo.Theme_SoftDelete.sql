CREATE PROCEDURE [dbo].[Theme_SoftDelete](
	@id int
) as
begin
	update dbo.Theme
	set
		IsDeleted=1
	where Id = @id
end
