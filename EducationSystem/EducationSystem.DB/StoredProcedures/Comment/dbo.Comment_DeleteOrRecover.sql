create proc [dbo].[Comment_DeleteOrRecover](
	@id int,
	@IsDeleted bit)
as
begin
	update dbo.Comment 
	set 
		IsDeleted = @IsDeleted
	where Id = @id
end