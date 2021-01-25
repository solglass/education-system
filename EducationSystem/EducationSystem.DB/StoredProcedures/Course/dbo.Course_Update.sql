CREATE proc [dbo].[Course_Update] (
@id int,
@name nvarchar(50),
@description nvarchar(MAX),
@duration int,
@isDeleted bit
)
as
begin 
	update dbo.Course
		set
		Name = @name,
		Description = @description,
		Duration = @duration,
		IsDeleted = @isDeleted
		where Id=@id
end