CREATE TABLE [dbo].[Student_Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[ContractNumber] [int] NOT NULL,
 CONSTRAINT [PK_STUDENT_GROUP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Student_Group]  WITH CHECK ADD  CONSTRAINT [Student_Group_fk0] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Student_Group] CHECK CONSTRAINT [Student_Group_fk0]
GO

ALTER TABLE [dbo].[Student_Group]  WITH CHECK ADD  CONSTRAINT [Student_Group_fk1] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Student_Group] CHECK CONSTRAINT [Student_Group_fk1]
GO

ALTER TABLE [dbo].[Student_Group]
ADD CONSTRAINT UC_StudentGroup_UserId_GroupId UNIQUE(UserId, GroupId)
GO