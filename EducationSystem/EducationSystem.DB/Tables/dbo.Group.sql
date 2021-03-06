CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
 CONSTRAINT [PK_GROUP] PRIMARY KEY CLUSTERED 
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

ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [Group_fk0] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [Group_fk0]
GO

ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [Group_fk1] FOREIGN KEY([StatusId])
REFERENCES [dbo].[GroupStatus] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [Group_fk1]
GO
