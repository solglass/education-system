CREATE  proc [dbo].[Tutor_Group_Delete] (
@userId int, @groupId int)
as
begin 
	delete from dbo.Tutor_Group
	where UserId=@userId and GroupId=groupId
end
