create proc [dbo].[Lesson_Update]( 
@id int,
@Description nvarchar(Max),
@Date datetime2)
as
begin
Update dbo.Lesson Set Description = @Description, Date = @Date Where Id=@id
end