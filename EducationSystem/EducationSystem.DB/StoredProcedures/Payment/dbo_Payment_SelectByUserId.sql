CREATE proc [dbo].[Payment_SelectByUserId](
	@id int
)
as
begin
	select
		p.Id,
		p.ContractNumber,		
		p.Amount,
		p.Date,
		p.Period,
		p.IsPaid,
		u.Id,
		u.FirstName,
		u.LastName,
		u.Phone,
		u.UserPic,
		u.Email
	from dbo.Payment p inner join dbo.[Student_Group] sg on p.ContractNumber=sg.ContractNumber inner join dbo.[User] u on sg.UserID=u.Id
	where u.Id = @id
end