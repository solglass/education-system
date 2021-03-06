CREATE proc [dbo].[Attachment_SelectById] (
@id int
) as begin
	select 
		a.Id, 
		a.Description,
		a.Path,
		at.Id
from [dbo].[Attachment] a
	inner join [dbo].[AttachmentType] at 
	on a.AttachmentTypeID = at.Id
	where a.Id = @id
end