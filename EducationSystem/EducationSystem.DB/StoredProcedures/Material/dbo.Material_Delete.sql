create proc [dbo].[Material_Delete] (
	@id int
) as
begin
	delete from dbo.Material
	where Id=@id
end