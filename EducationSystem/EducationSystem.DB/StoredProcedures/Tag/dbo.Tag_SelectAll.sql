create proc [dbo].[Tag_SelectAll]
as
begin 
	select
		Id,
		[Name]
	from dbo.Tag
end