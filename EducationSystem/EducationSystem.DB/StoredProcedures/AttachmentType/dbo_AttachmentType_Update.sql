create proc [dbo].[AttachmentType_Update] (
@id int,
@name nvarchar(15)
) as
begin
	update [dbo].[AttachmentType] 
	set 
		Name = @name
	where Id = @id
end 