create proc [dbo].[Feedback_Add](
@UserId int,
@Message nvarchar(Max),
@LessonID int,
@UnderstandingLevelId int)
as
begin
Insert Into dbo.Feedback(UserID,Message,LessonID,UnderstandingLevelId) Values(@UserId,@Message,@LessonID,@UnderstandingLevelId)
select SCOPE_IDENTITY()
end