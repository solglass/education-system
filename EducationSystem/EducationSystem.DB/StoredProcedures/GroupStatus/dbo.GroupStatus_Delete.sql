create proc [dbo].[GroupStatus_Delete] (
	@id int
 ) as
 begin
	delete from dbo.GroupStatus
	where Id = @id
 end
GO




