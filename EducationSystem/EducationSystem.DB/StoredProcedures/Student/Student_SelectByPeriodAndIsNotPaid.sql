CREATE proc [dbo].[Student_SelectByPeriodAndIsNotPaid] (@period nvarchar(7))
as
begin
	select
		u.FirstName,
		u.LastName,
		u.Phone,
		u.Email
	from
			(select
				p.ContractNumber,
				sum (cast (p.IsPaid as int)) as isPaid
			from dbo.[Payment] p 
			where p.Period= @period
			group by p.ContractNumber) as t
		inner join dbo.[Student_Group] sg on t.ContractNumber=sg.ContractNumber
		inner join dbo.[User] u on sg.UserID=u.Id
	where t.isPaid=0
end
