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
		sg.UserId,
		sg.GroupID,
		sg.ContractNumber,
		u.FirstName,
		u.LastName,
		u.BirthDate,
		u.Login,
		u.Password,
		u.Phone,
		u.UserPic,
		u.Email,
		u.RegistrationDate,
		u.IsDeleted
	from dbo.Payment p inner join dbo.[Student_Group] sg on p.ContractNumber=sg.ContractNumber
	                   inner join dbo.[User] u on sg.UserID=u.Id
where p.ContractNumber = @ContractNumber
end





