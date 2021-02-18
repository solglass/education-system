﻿CREATE TABLE [dbo].[HomeworkAttempt_Attachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HomeworkAttemptID] [int] NOT NULL,
	[AttachmentID] [int] NOT NULL,
 CONSTRAINT [PK_HOMEWORKATTEMPT_ATTACHMENT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
