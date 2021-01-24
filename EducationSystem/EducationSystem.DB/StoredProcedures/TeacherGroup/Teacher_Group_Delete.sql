create proc [dbo].[Teacher_Group_Delete]
	@id int
as
begin
	delete from dbo.Teacher_Group
	where Id = @id
end
