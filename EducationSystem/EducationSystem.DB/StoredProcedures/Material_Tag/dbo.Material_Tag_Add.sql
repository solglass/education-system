create proc [dbo].[Material_Tag_Add] (
	@tagId int,
	@materialId int
) as
begin
	insert into dbo.Material_Tag(TagId,MaterialId)
	values (@tagId,@materialId)
	select SCOPE_IDENTITY()
end