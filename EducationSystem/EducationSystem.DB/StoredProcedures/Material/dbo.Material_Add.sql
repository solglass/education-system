create proc [dbo].[Material_Add] 
(
	@link nvarchar(1000),
	@description nvarchar(max)
) as
begin
	insert into dbo.Material (Link, Description, isDeleted)
	values (@link, @description, 0)
	select SCOPE_IDENTITY()
end