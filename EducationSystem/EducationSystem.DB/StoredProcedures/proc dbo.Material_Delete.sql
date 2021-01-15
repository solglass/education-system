USE [DevEdu]
GO
/****** Object:  StoredProcedure [dbo].[Material_Delete]    Script Date: 12.01.2021 19:23:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Material_Delete] (
	@id int
) as
begin
	update dbo.Material
	set
		IsDeleted = 1
	where Id = @id
end