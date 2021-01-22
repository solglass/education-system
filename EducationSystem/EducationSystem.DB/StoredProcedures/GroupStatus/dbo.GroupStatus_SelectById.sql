create proc [dbo].[GroupStatus_SelectById] (
	@id int
 ) as
 begin
	select
		Id,
		Name
	from dbo.GroupStatus
	where Id = @id
 end
GO





