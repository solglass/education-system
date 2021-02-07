create proc [dbo].[Lesson_Update]( 
@id int,
@GroupID int,
@Description nvarchar(Max),
@Date datetime2,
@IsDeleted bit)
as
begin
Update dbo.Lesson Set GroupID = @GroupID,Description = @Description, Date = @Date,IsDeleted = @IsDeleted Where Id=@id
end