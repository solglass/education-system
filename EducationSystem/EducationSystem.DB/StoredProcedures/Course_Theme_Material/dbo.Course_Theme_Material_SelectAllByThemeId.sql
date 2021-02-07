create proc [dbo].[Course_Theme_Material_SelectAllByThemeId]
(@id int) as
begin
	select ctm.Id,
			ctm.CourseThemeID,
			ctm.MaterialID

	from dbo.Course_Theme ct left join dbo.Course_Theme_Material ctm on ctm.CourseThemeID=ct.Id
	where ct.ThemeID=@id
end 