CREATE PROCEDURE [dbo].[User_SelectStudentsByGroupId]
(
	@groupId int
)
as
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
      r.Id,
	  r.Name as [Role]
  FROM [dbo].[User] as u
    inner join dbo.[User_Role] as ur on u.Id = ur.UserID
    inner join dbo.[Role] as r on ur.RoleID = r.Id
    inner join dbo.[Student_Group] as sg on sg.UserId = u.Id
  where u.IsDeleted = 0 and sg.GroupId = @groupId
end
