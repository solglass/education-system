USE [DevEdu]
GO
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 07.02.2021 15:41:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[User_Delete]
(
	@id int
)
as
begin
	UPDATE dbo.[User] SET IsDeleted=1 WHERE Id=@id
end