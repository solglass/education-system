create  proc [dbo].[AttachmentType_SelectById] (
@id int
) as
begin
	select Id, 
	Name 
	from [dbo].[AttachmentType]
	where Id = @id
end