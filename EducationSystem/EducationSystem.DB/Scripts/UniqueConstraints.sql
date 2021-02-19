ALTER TABLE [dbo].[Material_Tag] 
ADD CONSTRAINT UC_MaterialId_TagId UNIQUE(MaterialId, TagId)
GO
ALTER TABLE [dbo].[Theme_Tag] 
ADD CONSTRAINT UC_TagId_ThemeId UNIQUE(TagId, ThemeId)
GO
ALTER TABLE [dbo].[Attendance]
ADD CONSTRAINT UC_LessonId_UserId UNIQUE(lessonId, userId)
GO
ALTER TABLE [dbo].[Homework_Theme]
ADD CONSTRAINT UC_HomeworkId_ThemeId UNIQUE(homeworkId, themeId)
GO
ALTER TABLE [dbo].[Theme] 
ADD CONSTRAINT UC_Name UNIQUE(Name)
GO
ALTER TABLE [dbo].[Tag] 
ADD CONSTRAINT UC_Name UNIQUE(Name)
GO
ALTER TABLE [dbo].[Group_Material] 
ADD CONSTRAINT UC_GroupID_MaterialID UNIQUE(GroupID,MaterialID)
GO
ALTER TABLE [dbo].[Teacher_Group] 
ADD CONSTRAINT UC_UserID_GroupID UNIQUE(UserID,GroupID)
GO

ALTER TABLE [dbo].[Homework_Tag] 
ADD CONSTRAINT UC_HomeworkId_TagId UNIQUE(HomeworkId, TagId)
GO

ALTER TABLE [dbo].[HomeworkAttempt]
ADD CONSTRAINT UC_HomeworkId_UserId UNIQUE(HomeworkId, UserId)
GO
ALTER TABLE [dbo].[Feedback]
ADD CONSTRAINT UC_LessonId_UserId UNIQUE(LessonId, UserId)
GO