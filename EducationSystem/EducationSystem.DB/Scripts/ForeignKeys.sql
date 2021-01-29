ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [Attachment_fk0] FOREIGN KEY([AttachmentTypeId])
REFERENCES [dbo].[AttachmentType] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [Attachment_fk0]
GO