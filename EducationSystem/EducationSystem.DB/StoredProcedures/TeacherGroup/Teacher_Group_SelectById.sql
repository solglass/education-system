create proc [dbo].[Teacher_Group_SelectById]
	@id int
as
begin
	select
		Id,
		UserID,
		GroupID
		from dbo.Teacher_Group
		where Id = @id
end
