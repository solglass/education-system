create proc [dbo].[GroupStatus_Update] (
	@id int,
	@name nvarchar(50)
 ) as
 begin
	update dbo.GroupStatus
	set
		Name = @name
	where Id = @id
 end
GO







