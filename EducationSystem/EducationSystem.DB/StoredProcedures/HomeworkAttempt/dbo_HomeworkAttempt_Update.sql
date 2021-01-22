CREATE proc [dbo].[HomeworkAttempt_Update] (
@id int,
@comment nvarchar(Max),
@userId int,
@homeworkId int,
@statusId int
)
as
begin
	update dbo.HomeworkAttempt
	set
		Comment = @comment,
		UserID = @userId,
		HomeworkID = @homeworkId,
		StatusID = @statusId
	where Id = @id
end