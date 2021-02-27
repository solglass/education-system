create proc [dbo].[Homework_HardDelete]
(
	@id int
)
as
begin
	delete dbo.Homework WHERE Id=@id
end