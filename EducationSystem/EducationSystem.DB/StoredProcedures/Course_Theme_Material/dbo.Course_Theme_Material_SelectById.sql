CREATE proc [dbo].[Course_Theme_Material_SelectById]
(@id int)
as
begin
Select Id, CourseThemeID,MaterialID
From dbo.Course_Theme_Material
where Id=@id
end
