create proc dbo.Material_SelectById (
	@id int
) as
begin
	select
		Id,
		Link,
		Description,
		IsDeleted
	from dbo.Material
	where Id = @id
end

