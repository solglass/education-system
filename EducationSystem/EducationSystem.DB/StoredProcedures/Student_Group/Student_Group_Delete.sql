create proc [dbo].[Student_Group_Delete](
	@UserId int,
	@GroupId int
) as
begin
	delete from dbo.Student_Group
	where UserId = @UserId and GroupId = @GroupId
end