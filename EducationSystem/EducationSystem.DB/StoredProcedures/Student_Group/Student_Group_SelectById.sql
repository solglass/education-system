create proc [dbo].[Student_Group_SelectById]
	@id int
as
begin
	select
		a.Id,
		a.ContractNumber, 
		u.Id,
		u.FirstName,
		u.LastName,
		u.Email,
		u.Phone,
		u.UserPic,
		g.Id,
		g.StartDate,
		g.StatusId
	from dbo.Student_Group a 
		inner join dbo.[User] u on u.ID=a.UserID
		inner join dbo.[Group] g on g.Id = a.GroupId
	where a.Id = @id 

end
