create proc [dbo].[Material_DeleteOrRecover] (
	@id int,
	@isDeleted bit
) as
begin
	update dbo.Material
	set
		IsDeleted = @isDeleted
	where Id = @id
end