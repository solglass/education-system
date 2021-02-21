CREATE proc [dbo].[HomeworkAttempt_SoftDeleteOrRecover] (
	@id int,
	@IsDeleted bit
)
as
begin
	update dbo.HomeworkAttempt
	set
		IsDeleted = @IsDeleted
	where Id = @id
end