CREATE TABLE [dbo].[Course_Theme_Material](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseThemeID] [int] NOT NULL,
	[MaterialID] [int] NOT NULL,
 CONSTRAINT [PK_COURSE_THEME_MATERIAL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF,
STATISTICS_NORECOMPUTE = OFF,
IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON
--, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY]
) ON [PRIMARY]
GO

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