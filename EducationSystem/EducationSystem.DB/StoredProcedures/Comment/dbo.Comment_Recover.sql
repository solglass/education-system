CREATE proc [dbo].[Comment_Recover](
	@id int
	)
as
begin
	update dbo.Comment 
	set 
		IsDeleted = 0
	where Id = @id
end