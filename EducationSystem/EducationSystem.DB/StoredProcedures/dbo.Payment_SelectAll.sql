USE [DevEdu]
GO

/****** Object:  StoredProcedure [dbo].[GroupStatus_SelectAll]    Script Date: 19.01.2021 0:39:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[GroupStatus_SelectAll]
as
begin
	select
		Id,
		Name
	from dbo.GroupStatus
end
GO

