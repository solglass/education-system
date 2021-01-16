CREATE proc [dbo].[Comment_Add](
@userId int, @homeworkAttemptId int, @message nvarchar(max))
as
begin
insert into dbo.Comment (UserID, HomeworkAttemptId, Message)
	values (@userId, @homeworkAttemptId, @message)
end