create proc [dbo].[Student_Group_Delete]
	@userID int,
	@groupID int,
	@contractNumber int
as
begin
	insert into dbo.Student_Group (UserID, GroupID, ContractNumber) 
	values(@userID, @groupID, @contractNumber)
end
