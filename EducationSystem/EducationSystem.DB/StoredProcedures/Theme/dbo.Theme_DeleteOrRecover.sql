CREATE PROCEDURE [dbo].[Theme_DeleteOrRecover](
	@id int,
	@IsDeleted bit
) as
begin
	update dbo.Theme
	set
		IsDeleted = @IsDeleted
	where Id = @id
end
