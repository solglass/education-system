create  [dbo].[AttachmentType_Add] (
@name nvarchar(15)
) as
begin
	insert into [dbo].[AttachmentType] (name) 
	values (@name)
end
