CREATE proc [dbo].[Homework_SoftDeleteOrRecover] (
	@id int,
	@IsDeleted bit)
 as
begin
	update dbo.Homework
	set
		IsDeleted = @IsDeleted
	where Id = @id
end