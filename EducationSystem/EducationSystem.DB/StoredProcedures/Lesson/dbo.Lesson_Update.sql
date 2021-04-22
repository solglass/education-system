create proc [dbo].[Lesson_Update]( 
@id int,
@Description nvarchar(Max),
@Date datetime2,
@RecordLink nvarchar(Max))
as
begin
Update dbo.Lesson Set Description = @Description, Date = @Date, RecordLink = @RecordLink Where Id=@id
end