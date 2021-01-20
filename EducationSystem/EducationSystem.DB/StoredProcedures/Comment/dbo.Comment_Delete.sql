CREATE proc [dbo].[Comment_Delete](
@id int)
as
begin
	update dbo.Comment 
	set 
		IsDeleted=1
	where Id=@id
end