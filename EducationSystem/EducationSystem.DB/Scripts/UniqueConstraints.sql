ALTER TABLE [dbo].[Material_Tag] 
ADD CONSTRAINT UC_MaterialId_TagId UNIQUE(materialId, tagId)
GO
ALTER TABLE [dbo].[Theme_Tag] 
ADD CONSTRAINT UC_TagId_ThemeId UNIQUE(tagId, themeId)
GO