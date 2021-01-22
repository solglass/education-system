create proc dbo.Material_Add (
	@link nvarchar(1000),
	@description nvarchar(max),
	@isDeleted bit
) as
begin
	insert into dbo.Material (Link, Description, IsDeleted)
	values (@link, @description, @isDeleted)
end