create proc [dbo].[Lesson_Add](
@GroupID int,
@Description nvarchar(Max),
@Date datetime2,
@RecordLink nvarchar(max))
as
	begin
		Insert Into dbo.Lesson(GroupID,Description,Date,RecordLink, IsDeleted) Values(@GroupID,@Description,@Date,@RecordLink, 0)
		select SCOPE_IDENTITY()
	end