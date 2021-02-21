CREATE proc [dbo].[Comment_SoftDeleteOrRecover](
	@id int,
	@IsDeleted bit)
as
begin
	update dbo.Comment 
	set 
		IsDeleted = @IsDeleted
	where Id = @id
end