CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Message] [nvarchar](1000) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [Notification_fk0] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [Notification_fk0]
GO

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [Notification_fk1] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [Notification_fk1]
GO
