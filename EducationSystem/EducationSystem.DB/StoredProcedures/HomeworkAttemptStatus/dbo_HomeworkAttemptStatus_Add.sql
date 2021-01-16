CREATE proc [dbo].[HomeworkAttemptStatus_Add] (
@name nvarchar
)
as
begin
	insert into dbo.HomeworkAttemptStatus (Name)
	values (@name)
end