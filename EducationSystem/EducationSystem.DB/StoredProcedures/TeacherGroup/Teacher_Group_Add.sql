create proc [dbo].[Teacher_Group_Add]
	@userID int,
	@groupID int
as
begin
	insert into dbo.Teacher_Group (UserID, GroupID)
	values (@userID, @groupID)
select SCOPE_IDENTITY()
end
