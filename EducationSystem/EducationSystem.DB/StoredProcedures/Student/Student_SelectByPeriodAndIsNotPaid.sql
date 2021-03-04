CREATE proc [dbo].[Student_SelectByPeriodAndIsNotPaid] (@period nvarchar(7))
as
begin
	select DISTINCT
		u.FirstName,
		u.LastName,
		u.Email,
		u.Phone
	from dbo.[Payment] p 
		inner join dbo.[Student_Group] sg on p.ContractNumber=sg.ContractNumber
		inner join dbo.[User] u on sg.UserID=u.Id
	where p.IsPaid=0 and p.Period = @period
end
