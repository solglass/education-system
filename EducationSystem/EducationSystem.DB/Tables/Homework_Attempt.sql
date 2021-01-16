CREATE TABLE [dbo].[HomeworkAttempt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[HomeworkId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_HOMEWORKATTEMPT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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
