create proc [dbo].[UnderstandingLevel_SelectByID](@id int)
as
begin
select Id,
	Name
	from dbo.UnderstandingLevel
	where Id = @id
end