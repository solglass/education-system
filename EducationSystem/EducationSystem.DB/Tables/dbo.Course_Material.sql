CREATE TABLE [dbo].[Course_Material]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[MaterialID] [int] NOT NULL,
 CONSTRAINT [PK_COURSE_Material] PRIMARY KEY CLUSTERED 
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

ALTER TABLE [dbo].[Course_Material]  WITH CHECK ADD  CONSTRAINT [Course_Material_fk0] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Course_Material] CHECK CONSTRAINT [Course_Material_fk0]
GO

ALTER TABLE [dbo].[Course_Material]  WITH CHECK ADD  CONSTRAINT [Course_Material_fk1] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Course_Material] CHECK CONSTRAINT [Course_Material_fk1]
GO

ALTER TABLE [dbo].[Course_Material]
ADD CONSTRAINT UC_CourseMaterial_CourseId_MaterialId UNIQUE(CourseId, MaterialId)
GO