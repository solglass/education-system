CREATE PROCEDURE [dbo].[Check_User_Authentication](
	@login nvarchar(50)
)as
begin
	select
		us.Id,
		us.Login,
		us.Password,
		r.Id,
		r.Name
	from dbo.[User] us
	inner join dbo.User_Role rl on us.Id = rl.UserID 
	inner join dbo.Role r on rl.RoleID = r.Id
	where us.Login = @login 
end
