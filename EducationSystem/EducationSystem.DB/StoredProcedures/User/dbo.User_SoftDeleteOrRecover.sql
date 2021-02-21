create proc [dbo].[User_SoftDeleteOrRecover]
(
	@id int,
	@IsDeleted bit
)
as
begin
	update dbo.[User]
	set IsDeleted=@IsDeleted
	where Id=@id
end