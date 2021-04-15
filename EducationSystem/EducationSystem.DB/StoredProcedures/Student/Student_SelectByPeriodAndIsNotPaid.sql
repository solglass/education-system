CREATE proc [dbo].[Student_SelectByPeriodAndIsNotPaid] (@period nvarchar(7))
as
begin

with pt as(select
			p.ContractNumber,
			sum (cast (p.IsPaid as int)) as isPaid
			from dbo.[Payment] p 
			where (p.Period= @period) 
			group by p.ContractNumber)
	select
		u.FirstName,
		u.LastName,
		u.Phone,
		u.Email,
		u.Id,
		ISNULL(pt.ispaid, 0)
	from
		dbo.[Student_Group] sg 
		inner join dbo.[User] u on sg.UserID=u.Id
		left join pt on sg.ContractNumber  =pt.ContractNumber
end
