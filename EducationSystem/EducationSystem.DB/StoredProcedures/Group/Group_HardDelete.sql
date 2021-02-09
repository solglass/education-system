create proc [dbo].[Group_HardDelete]
(
	@id int
)
as
begin
	delete dbo.[Group] WHERE Id=@id
end