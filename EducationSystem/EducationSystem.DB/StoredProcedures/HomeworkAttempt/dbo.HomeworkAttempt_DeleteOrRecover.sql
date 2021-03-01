create proc [dbo].[HomeworkAttempt_DeleteOrRecover] (
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