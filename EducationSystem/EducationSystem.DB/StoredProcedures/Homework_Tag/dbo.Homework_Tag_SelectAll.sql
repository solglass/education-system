USE [DevEdu]
GO
/****** Object:  StoredProcedure [dbo].[Homework_Tag_SelectAll]    Script Date: 07.02.2021 15:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Homework_Tag_SelectAll]
as
begin 

			select
		ht.Id,
		ht.TagId,
		t.Name,
		ht.HomeworkId
		from dbo.Homework_Tag as ht inner join dbo.Tag as t on ht.Id = t.Id
end