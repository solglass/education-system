create proc [dbo].[Material_Tag_Delete](
@materialId int,
@tagId int)
as
delete from dbo.Material_Tag 
where MaterialId = @materialId and TagId = @tagId
