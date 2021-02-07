USE [DevEdu]
GO
/****** Object:  StoredProcedure [dbo].[Homework_Tag_Update]    Script Date: 07.02.2021 15:25:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Homework_Tag_Update] (
      @id int,
	  @tagid int,
	  @Homeworkid int
) as
begin
  update dbo.Homework_Tag
  set
  TagId=@tagid,
  HomeworkId=@Homeworkid
  where Id=@id
end