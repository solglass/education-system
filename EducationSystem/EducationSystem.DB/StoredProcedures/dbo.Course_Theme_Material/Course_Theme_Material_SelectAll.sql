CREATE proc [dbo].[Course_Theme_Material_SelectAll]

as
begin
Select Id,CourseThemeID,MaterialID
from dbo.Course_Theme_Material
end
