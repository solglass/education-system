CREATE proc [dbo].[Payment_SelectAll]
as
begin
	select
		p.Id,
		p.ContractNumber,		
		p.Amount,
		p.Date,
		p.Period,
		p.IsPaid,
		sg.UserId,
		sg.GroupID,
		sg.ContractNumber,
		u.Id,
		u.FirstName,
		u.LastName,	
		u.UserPic		
	from dbo.Payment p inner join dbo.[Student_Group] sg on p.ContractNumber=sg.ContractNumber
	                   inner join dbo.[User] u on sg.UserID=u.Id
end




