create proc [dbo].[Tag_SelectById](
	@id int
) as
begin
	select	Id,
			[Name]
	from dbo.Tag
	where Id = @id
end