﻿CREATE TABLE [dbo].[User_Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_USER_ROLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_UserId_RoleId] UNIQUE NONCLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [User_Role_fk0] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [User_Role_fk0]
GO

ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [User_Role_fk1] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([Id])
ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [User_Role_fk1]
GO