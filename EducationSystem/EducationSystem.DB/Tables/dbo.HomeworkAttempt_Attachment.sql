CREATE TABLE [dbo].[HomeworkAttempt_Attachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HomeworkAttemptID] [int] NOT NULL,
	[AttachmentID] [int] NOT NULL,
 CONSTRAINT [PK_HOMEWORKATTEMPT_ATTACHMENT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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

ALTER TABLE [dbo].[HomeworkAttempt]
ADD CONSTRAINT UC_HomeworkId_UserId UNIQUE(HomeworkId, UserId)
GO

ALTER TABLE [dbo].[HomeworkAttempt_Attachment]
ADD CONSTRAINT UC_HomeworkAttemptId_AttachmentId UNIQUE(HomeworkAttemptId, AttachmentId)
GO