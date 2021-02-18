ALTER TABLE [dbo].[Material_Tag] 
ADD CONSTRAINT UC_MaterialId_TagId UNIQUE(materialId, tagId)
GO
ALTER TABLE [dbo].[Theme_Tag] 
ADD CONSTRAINT UC_TagId_ThemeId UNIQUE(tagId, themeId)
GO
ALTER TABLE [dbo].[Attendance]
ADD CONSTRAINT UC_LessonId_UserId UNIQUE(lessonId, userId)
GO
ALTER TABLE [dbo].[Homework_Theme]
ADD CONSTRAINT UC_HomeworkId_ThemeId UNIQUE(homeworkId, themeId)
GO
ALTER TABLE [dbo].[User_Role]
ADD CONSTRAINT UC_UserId_RoleId UNIQUE (userId, roleId)
GO