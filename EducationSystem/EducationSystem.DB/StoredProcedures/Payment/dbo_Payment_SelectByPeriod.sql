CREATE proc [dbo].[Payment_SelectByPeriod](
	@periodFrom nvarchar(7),
	@periodTo nvarchar(7)
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
		u.UserPic
	from dbo.Payment p inner join dbo.[Student_Group] sg on p.ContractNumber=sg.ContractNumber inner join dbo.[User] u on sg.UserID=u.Id
	where p.Period >= @periodFrom and p.Period <= @periodTo
end