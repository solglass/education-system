create proc [dbo].[User_SelectAll] as
begin
	SELECT  
	u.[Id]
      ,u.[FirstName]
      ,u.[LastName]
      ,u.[BirthDate]
      ,u.[Login]
      ,u.[Password]
      ,u.[Phone]
      ,u.[UserPic]
      ,u.[Email],
      (select distinct		
		g.ContractNumber		
	  from dbo.Student_Group as g
	  where u.Id = g.UserId) as ContractNumber,
      r.Id,
	  r.Name as [Role]
  FROM [dbo].[User] as u
  inner join dbo.[User_Role] as ur on u.Id = ur.UserID
  inner join dbo.[Role] as r on ur.RoleID = r.Id 
  where u.IsDeleted = 0
end