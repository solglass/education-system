﻿create proc [dbo].[Lesson_Update]( 
@id int,
@Comment nvarchar(Max),
@Date datetime2)
as
begin
Update dbo.Lesson Set Description = @Comment, Date = @Date Where Id=@id
end