create proc [dbo].[Teacher_Group_SelectAll]	
as
begin
	select
		Id,
		UserID,
		GroupID
		from dbo.Teacher_Group		
end

