create proc [dbo].[Role_SelectAll] as
begin
	select Id, Name from dbo.Role
end