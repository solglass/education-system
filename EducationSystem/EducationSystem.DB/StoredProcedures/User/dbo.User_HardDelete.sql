create proc [dbo].[User_HardDelete]
(
	@id int
)
as
begin
	delete dbo.[User] where Id=@id
end