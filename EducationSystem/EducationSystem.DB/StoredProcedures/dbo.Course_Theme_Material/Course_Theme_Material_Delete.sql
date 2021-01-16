CREATE proc [dbo].[Course_Theme_Material_Delete]
(@id int)
as 
begin
	delete from dbo.Course_Theme_Material
	where Id=@id
end
