create proc [dbo].[Teacher_Group_Delete]
	(
	@userId int,
    @groupId int 
)as
begin
	delete from dbo.Teacher_Group
	 where UserID =  @userId and GroupID = @groupId
end
