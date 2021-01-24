create proc [dbo].[Student_Group_SelectAll]	
as
begin
		Id,
		UserId,
		GroupId,
		ContractNumber
	from dbo.Student_Group
end
