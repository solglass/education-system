CREATE proc [dbo].[Comment_Update]
(@id int,  @message nvarchar(max))
as
begin
	update dbo.Comment 
	set Message = @message
	where Id=@id
end