CREATE proc [dbo].[HomeworkAttempt_Update] (
@id int,
@comment nvarchar(Max),
@statusId int
)
as
begin
	update dbo.HomeworkAttempt
	set
		Comment = @comment,
		StatusID = @statusId
	where Id = @id
end