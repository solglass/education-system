ALTER TABLE [dbo].[Material_Tag] 
ADD CONSTRAINT UC_MaterialId_TagId unique(materialId, tagId)
GO