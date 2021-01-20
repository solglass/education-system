CREATE proc [dbo].[HomeworkAttempt_Add] (
@comment nvarchar(Max),
@userId int,
@homeworkId int,
@statusId int
)
as
begin
	insert into dbo.HomeworkAttempt (Comment, UserID, HomeworkID, StatusID, IsDeleted)
	values (@comment, @userId, @homeworkId, @statusId, 0)
end