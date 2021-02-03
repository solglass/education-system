create proc [dbo].[Student_Group_SelectAll]	
as
begin
select
		Id,
		UserId,
		GroupId,
		ContractNumber
	from dbo.Student_Group
end
