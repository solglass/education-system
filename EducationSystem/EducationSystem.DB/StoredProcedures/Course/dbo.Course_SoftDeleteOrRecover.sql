CREATE proc [dbo].[Course_SoftDeleteOrRecover] (
	@id int,
	@IsDeleted bit
) as
begin
	update dbo.Course
	set
		IsDeleted=@IsDeleted
	where Id = @id
end
