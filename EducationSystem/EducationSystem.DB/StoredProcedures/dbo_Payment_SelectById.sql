create proc dbo.Payment_SelectById (@id int)
as
begin
	select
		p.Id,
		p.ContractNumber,		
		p.Amount,
		p.Date,
		p.Period,
		p.IsPaid,
		sg.GroupID,
		sg.UserID
	from [dbo].[Payment] p inner join [dbo].[Student_Group] sg on p.ContractNumber=sg.ContractNumber
where p.Id = @id
end