CREATE proc [dbo].[Payment_SelectByContractNumber] (@ContractNumber int)
as
begin
	select
		p.Id,
		p.ContractNumber,		
		p.Amount,
		p.Date,
		p.Period,
		p.IsPaid,
		p.ContractNumber,
		u.Id,
		u.Phone,
		u.Email,
		u.FirstName,
		u.LastName,		
		u.UserPic
	from dbo.Payment p inner join dbo.[Student_Group] sg on p.ContractNumber=sg.ContractNumber
	                   inner join dbo.[User] u on sg.UserID=u.Id
where p.ContractNumber = @ContractNumber
order by p.Id
end





