CREATE TABLE [dbo].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[LessonID] [int] NOT NULL,
	[UnderstandingLevelId] [int] NOT NULL,
 CONSTRAINT [PK_FEEDBACK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Lesson_Theme]  WITH CHECK ADD  CONSTRAINT [Lesson_Theme_fk0] FOREIGN KEY([ThemeID])
REFERENCES [dbo].[Theme] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Lesson_Theme] CHECK CONSTRAINT [Lesson_Theme_fk0]
GO

ALTER TABLE [dbo].[Lesson_Theme]  WITH CHECK ADD  CONSTRAINT [Lesson_Theme_fk1] FOREIGN KEY([LessonID])
REFERENCES [dbo].[Lesson] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Lesson_Theme] CHECK CONSTRAINT [Lesson_Theme_fk1]
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

ALTER TABLE [dbo].[Feedback]
ADD CONSTRAINT UC_Feedback_LessonId_UserId UNIQUE(LessonId, UserId)
GO