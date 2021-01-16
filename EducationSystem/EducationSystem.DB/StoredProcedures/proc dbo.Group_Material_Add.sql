create proc dbo.Group_Material_Add (
	@groupID int,
	@materialID int
) as
begin
	insert into dbo.Group_Material (GroupID, MaterialID)
	values (@groupID, @materialID)
end