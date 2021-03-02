create proc [dbo].[Comment_HardDelete]
(
	@id int
)
as
begin
	delete dbo.Comment WHERE Id=@id
end