create proc [dbo].[UnderstandingLevel_Add](
@Name nvarchar(100))
as
begin
	Insert Into dbo.UnderstandingLevel(Name) Values(@Name)
	select SCOPE_IDENTITY() as LastId
end