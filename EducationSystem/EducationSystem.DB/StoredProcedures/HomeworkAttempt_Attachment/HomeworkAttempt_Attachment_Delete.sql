CREATE proc [dbo].[HomeworkAttempt_Attachment_Delete] (
@id int)
as
begin
	delete from [dbo].[HomeworkAttempt_Attachment]
	where Id = @id
end
