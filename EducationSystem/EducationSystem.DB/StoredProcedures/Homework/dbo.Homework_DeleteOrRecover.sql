create proc [dbo].[Homework_DeleteOrRecover] (
	@id int,
	@IsDeleted bit)
 as
begin
	update dbo.Homework
	set
		IsDeleted = @IsDeleted
	where Id = @id
end