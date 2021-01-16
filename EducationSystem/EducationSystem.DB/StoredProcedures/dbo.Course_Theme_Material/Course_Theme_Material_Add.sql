CREATE proc [dbo].[Course_Theme_Material_Add]
(@courseThemeID int, @materialID int)
as
begin
Insert into dbo.Course_Theme_Material (CourseThemeID,MaterialID)
Values (@courseThemeID,@materialID)
end
