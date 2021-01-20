create proc [dbo].[Payment_SelectByContractNumber] (@ContractNumber int)
as
begin
	select
		p.Id,
		p.ContractNumber,		
		/*ContractNumber,*/
		p.Amount,
		p.Date,
		p.Period,
		p.IsPaid,
		sg.GroupID,
		sg.UserID
	from dbo.Payment p inner join dbo.Student_Group sg on p.ContractNumber=sg.ContractNumber
where p.ContractNumber = @ContractNumber
end