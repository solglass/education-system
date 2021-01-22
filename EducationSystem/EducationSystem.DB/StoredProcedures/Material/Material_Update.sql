create proc dbo.Material_Update (
	@id int,
	@link nvarchar(1000),
	@description nvarchar(max)
) as
begin
	update dbo.Material
	set
		Link = @link,
		Description = @description
	where Id = @id
end