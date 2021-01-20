CREATE proc [dbo].[Comment_Update]
(@id int, @userId int, @homeworkAttemptId int, @message nvarchar(max), @isDeleted bit)
as
begin
	update dbo.Comment 
	set
		UserID=@userId,
		HomeworkAttemptId=@homeworkAttemptId,
		Message=@message,
		IsDeleted=@isDeleted
	where Id=@id
end