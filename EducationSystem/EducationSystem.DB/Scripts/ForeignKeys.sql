ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [Attachment_fk0] FOREIGN KEY([AttachmentTypeId])
REFERENCES [dbo].[AttachmentType] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [Attachment_fk0]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [Comment_fk0] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [Comment_fk0]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [Comment_fk1] FOREIGN KEY([HomeworkAttemptId])
REFERENCES [dbo].[HomeworkAttempt] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [Comment_fk1]


ALTER TABLE [dbo].[HomeworkAttempt]  WITH CHECK ADD  CONSTRAINT [HomeworkAttempt_fk0] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[HomeworkAttempt] CHECK CONSTRAINT [HomeworkAttempt_fk0]
GO

ALTER TABLE [dbo].[HomeworkAttempt]  WITH CHECK ADD  CONSTRAINT [HomeworkAttempt_fk1] FOREIGN KEY([HomeworkId])
REFERENCES [dbo].[Homework] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[HomeworkAttempt] CHECK CONSTRAINT [HomeworkAttempt_fk1]
GO

ALTER TABLE [dbo].[HomeworkAttempt]  WITH CHECK ADD  CONSTRAINT [HomeworkAttempt_fk2] FOREIGN KEY([StatusId])
REFERENCES [dbo].[HomeworkAttemptStatus] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[HomeworkAttempt] CHECK CONSTRAINT [HomeworkAttempt_fk2]
GO


ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [Group_fk0] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [Group_fk0]
GO

ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [Group_fk1] FOREIGN KEY([StatusId])
REFERENCES [dbo].[GroupStatus] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [Group_fk1]
GO


ALTER TABLE [dbo].[Group_Material]  WITH CHECK ADD  CONSTRAINT [Group_Material_fk0] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Group_Material] CHECK CONSTRAINT [Group_Material_fk0]
GO

ALTER TABLE [dbo].[Group_Material]  WITH CHECK ADD  CONSTRAINT [Group_Material_fk1] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Group_Material] CHECK CONSTRAINT [Group_Material_fk1]
GO


ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [Attendance_fk0] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lesson] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [Attendance_fk0]
GO

ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [Attendance_fk1] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [Attendance_fk1]
GO


ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [Feedback_fk0] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [Feedback_fk0]
GO

ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [Feedback_fk1] FOREIGN KEY([LessonID])
REFERENCES [dbo].[Lesson] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [Feedback_fk1]
GO

ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [Feedback_fk2] FOREIGN KEY([UnderstandingLevelId])
REFERENCES [dbo].[UnderstandingLevel] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [Feedback_fk2]
GO

ALTER TABLE [dbo].[Homework_Tag]  WITH CHECK ADD  CONSTRAINT [Homework_Tag_fk0] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Homework_Tag] CHECK CONSTRAINT [Homework_Tag_fk0]
GO

ALTER TABLE [dbo].[Homework_Tag]  WITH CHECK ADD  CONSTRAINT [Homework_Tag_fk1] FOREIGN KEY([HomeworkId])
REFERENCES [dbo].[Homework] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Homework_Tag] CHECK CONSTRAINT [Homework_Tag_fk1]
GO

ALTER TABLE [dbo].[HomeworkAttempt_Attachment]  WITH CHECK ADD  CONSTRAINT [HomeworkAttempt_Attachment_fk0] FOREIGN KEY([HomeworkAttemptID])
REFERENCES [dbo].[HomeworkAttempt] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[HomeworkAttempt_Attachment] CHECK CONSTRAINT [HomeworkAttempt_Attachment_fk0]
GO

ALTER TABLE [dbo].[HomeworkAttempt_Attachment]  WITH CHECK ADD  CONSTRAINT [HomeworkAttempt_Attachment_fk1] FOREIGN KEY([AttachmentID])
REFERENCES [dbo].[Attachment] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[HomeworkAttempt_Attachment] CHECK CONSTRAINT [HomeworkAttempt_Attachment_fk1]
GO

ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [Lesson_fk0] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [Lesson_fk0]
GO

ALTER TABLE [dbo].[Material_Tag]  WITH CHECK ADD  CONSTRAINT [Material_Tag_fk0] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Material_Tag] CHECK CONSTRAINT [Material_Tag_fk0]
GO

ALTER TABLE [dbo].[Material_Tag]  WITH CHECK ADD  CONSTRAINT [Material_Tag_fk1] FOREIGN KEY([MaterialId])
REFERENCES [dbo].[Material] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Material_Tag] CHECK CONSTRAINT [Material_Tag_fk1]
GO

ALTER TABLE [dbo].[Student_Group]  WITH CHECK ADD  CONSTRAINT [Student_Group_fk0] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Student_Group] CHECK CONSTRAINT [Student_Group_fk0]
GO

ALTER TABLE [dbo].[Student_Group]  WITH CHECK ADD  CONSTRAINT [Student_Group_fk1] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Student_Group] CHECK CONSTRAINT [Student_Group_fk1]
GO

ALTER TABLE [dbo].[Teacher_Group]  WITH CHECK ADD  CONSTRAINT [Teacher_Group_fk0] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Teacher_Group] CHECK CONSTRAINT [Teacher_Group_fk0]
GO

ALTER TABLE [dbo].[Teacher_Group]  WITH CHECK ADD  CONSTRAINT [Teacher_Group_fk1] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Teacher_Group] CHECK CONSTRAINT [Teacher_Group_fk1]
GO

ALTER TABLE [dbo].[Theme_Tag]  WITH CHECK ADD  CONSTRAINT [Theme_Tag_fk0] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Theme_Tag] CHECK CONSTRAINT [Theme_Tag_fk0]
GO

ALTER TABLE [dbo].[Theme_Tag]  WITH CHECK ADD  CONSTRAINT [Theme_Tag_fk1] FOREIGN KEY([ThemeId])
REFERENCES [dbo].[Theme] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Theme_Tag] CHECK CONSTRAINT [Theme_Tag_fk1]
GO

ALTER TABLE [dbo].[Tutor_Group]  WITH CHECK ADD  CONSTRAINT [Tutor_Group_fk0] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Tutor_Group] CHECK CONSTRAINT [Tutor_Group_fk0]
GO

ALTER TABLE [dbo].[Tutor_Group]  WITH CHECK ADD  CONSTRAINT [Tutor_Group_fk1] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Tutor_Group] CHECK CONSTRAINT [Tutor_Group_fk1]
GO

ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [User_Role_fk0] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [User_Role_fk0]
GO

ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [User_Role_fk1] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [User_Role_fk1]
GO
