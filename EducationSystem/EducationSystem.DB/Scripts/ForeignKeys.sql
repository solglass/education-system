ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [Attachment_fk0] FOREIGN KEY([AttachmentTypeId])
REFERENCES [dbo].[AttachmentType] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [Attachment_fk0]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [Comment_fk0] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [Comment_fk0]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [Comment_fk1] FOREIGN KEY([HomeworkAttemptId])
REFERENCES [dbo].[HomeworkAttempt] ([Id])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [Comment_fk1]


ALTER TABLE [dbo].[Course_Theme_Material]  WITH CHECK ADD  CONSTRAINT [Course_Theme_Material_fk0] FOREIGN KEY([CourseThemeID])
REFERENCES [dbo].[Course_Theme] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Course_Theme_Material] CHECK CONSTRAINT [Course_Theme_Material_fk0]
GO

ALTER TABLE [dbo].[Course_Theme_Material]  WITH CHECK ADD  CONSTRAINT [Course_Theme_Material_fk1] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Course_Theme_Material] CHECK CONSTRAINT [Course_Theme_Material_fk1]
GO


ALTER TABLE [dbo].[HomeworkAttempt]  WITH CHECK ADD  CONSTRAINT [HomeworkAttempt_fk0] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[HomeworkAttempt] CHECK CONSTRAINT [HomeworkAttempt_fk0]
GO

ALTER TABLE [dbo].[HomeworkAttempt]  WITH CHECK ADD  CONSTRAINT [HomeworkAttempt_fk1] FOREIGN KEY([HomeworkId])
REFERENCES [dbo].[Homework] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[HomeworkAttempt] CHECK CONSTRAINT [HomeworkAttempt_fk1]
GO

ALTER TABLE [dbo].[HomeworkAttempt]  WITH CHECK ADD  CONSTRAINT [HomeworkAttempt_fk2] FOREIGN KEY([StatusId])
REFERENCES [dbo].[HomeworkAttemptStatus] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[HomeworkAttempt] CHECK CONSTRAINT [HomeworkAttempt_fk2]
GO
