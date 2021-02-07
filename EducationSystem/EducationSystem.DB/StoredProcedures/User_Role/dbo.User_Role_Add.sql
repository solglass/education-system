USE [DevEdu]
GO
/****** Object:  StoredProcedure [dbo].[User_Role_Add]    Script Date: 07.02.2021 15:42:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[User_Role_Add] (
	@userid int,
	@roleid int
) as
begin
	INSERT INTO dbo.User_Role (UserID, RoleID) VALUES(@userid, @roleid)
end