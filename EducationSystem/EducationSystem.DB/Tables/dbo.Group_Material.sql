CREATE TABLE [dbo].[Group_Material](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NOT NULL,
	[MaterialID] [int] NOT NULL,
 CONSTRAINT [PK_GROUP_MATERIAL] PRIMARY KEY CLUSTERED 
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

ALTER TABLE [dbo].[Group_Material]  WITH CHECK ADD  CONSTRAINT [Group_Material_fk0] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Group_Material] CHECK CONSTRAINT [Group_Material_fk0]
GO

ALTER TABLE [dbo].[Group_Material]  WITH CHECK ADD  CONSTRAINT [Group_Material_fk1] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Group_Material] CHECK CONSTRAINT [Group_Material_fk1]
GO

ALTER TABLE [dbo].[Group_Material] 
ADD CONSTRAINT UC_GroupID_MaterialID UNIQUE(GroupID,MaterialID)
GO