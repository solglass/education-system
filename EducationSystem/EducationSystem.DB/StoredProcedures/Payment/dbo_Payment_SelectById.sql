CREATE proc [dbo].[Payment_SelectById](
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
		u.BirthDate,
		u.Login,
		u.Password,
		u.Phone,
		u.UserPic,
		u.Email,
		u.RegistrationDate,
		u.IsDeleted
	from dbo.Payment p inner join dbo.[Student_Group] sg on p.ContractNumber=sg.ContractNumber inner join dbo.[User] u on sg.UserID=u.Id
	where p.Id = @id
end


