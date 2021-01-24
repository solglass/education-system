create proc [dbo].[Student_Group_SelectById]
	@id int
as
begin
	select
		Id,
		UserID,
		GroupID,
		ContractNumber
	from dbo.Student_Group a 
	where a.Id = @id 

end
