CREATE TABLE [dbo].[Theme_Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TagId] [int] NOT NULL,
	[ThemeId] [int] NOT NULL,
 CONSTRAINT [PK_THEME_TAG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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

ALTER TABLE [dbo].[Theme_Tag] 
ADD CONSTRAINT UC_TagId_ThemeId UNIQUE(TagId, ThemeId)
GO