CREATE TABLE [dbo].[Comment_Attachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentId] [int] NOT NULL,
	[AttachmentId] [int] NOT NULL,
 CONSTRAINT [PK_COMMENT_ATTACHMENT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Comment_Attachment]  WITH CHECK ADD  CONSTRAINT [Comment_Attachment_fk0] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comment] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Comment_Attachment] CHECK CONSTRAINT [Comment_Attachment_fk0]
GO

ALTER TABLE [dbo].[Comment_Attachment]  WITH CHECK ADD  CONSTRAINT [Comment_Attachment_fk1] FOREIGN KEY([AttachmentId])
REFERENCES [dbo].[Attachment] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Comment_Attachment] CHECK CONSTRAINT [Comment_Attachment_fk1]
GO

