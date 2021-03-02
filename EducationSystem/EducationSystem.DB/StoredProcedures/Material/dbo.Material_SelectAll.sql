create proc dbo.Material_SelectAll
as
begin
	select
		Id,
		Link,
		Description,
		IsDeleted
	from dbo.Material
end


