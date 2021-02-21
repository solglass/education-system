create proc [dbo].[Material_SoftDeleteOrRecover] (
	@id int,
	@IsDeleted bit
) as
begin
	update dbo.Material
	set
		IsDeleted = @IsDeleted
	where Id = @id
end