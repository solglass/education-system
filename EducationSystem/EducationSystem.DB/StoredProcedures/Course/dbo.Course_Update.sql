CREATE proc [dbo].[Course_Update] (
@id int,
@name nvarchar(50),
@description nvarchar(MAX),
@duration int
)
as
begin 
	update dbo.Course
		set
		Name = @name,
		Description = @description,
		Duration = @duration
		where Id=@id
end