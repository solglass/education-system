create proc [dbo].[Material_HardDelete] (
	@id int
) as
begin
	delete from dbo.Material
	where Id=@id
end