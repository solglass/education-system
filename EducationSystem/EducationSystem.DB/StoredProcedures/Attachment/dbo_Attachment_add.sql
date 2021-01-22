create proc dbo.Attachment_Add (
@path nvarchar(250)
) as
begin
	insert into [dbo].[Attachment] (Path) 
	values (@path)
end 