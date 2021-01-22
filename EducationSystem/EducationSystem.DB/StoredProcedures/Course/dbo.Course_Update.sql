CREATE proc [dbo].[Course_Update] (
@id int,
@Name nvarchar(50),
@Description nvarchar(MAX),
@Duration int,
@IsDeleted bit
)
as
begin 
	update dbo.Course
		set
		Name = @Name,
		Description = @Description,
		Duration = @Duration,
		IsDeleted = @IsDeleted
		where Id=@id
end