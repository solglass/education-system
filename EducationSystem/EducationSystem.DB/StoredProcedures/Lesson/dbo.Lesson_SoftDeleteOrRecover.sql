create proc [dbo].[Lesson_SoftDeleteOrRecover]
( @Id int,
  @IsDeleted bit)
as
begin
	update dbo.Lesson
	set
	IsDeleted = @IsDeleted
	where Id=@id
end