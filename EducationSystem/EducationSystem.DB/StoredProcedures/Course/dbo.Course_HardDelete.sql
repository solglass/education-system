create proc [dbo].[Course_HardDelete]
(
	@id int
)
as
begin
	delete dbo.Course WHERE Id=@id
end