create proc [dbo].[Course_DeleteOrRecover] (
	@id int,
	@IsDeleted bit)
as
begin
	update dbo.Course
	set
		IsDeleted = @IsDeleted
	where Id = @id
end