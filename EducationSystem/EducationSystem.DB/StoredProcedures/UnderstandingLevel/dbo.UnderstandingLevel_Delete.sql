create proc [dbo].[UnderstandingLevel_Delete](@Id int)
as
begin
Delete from dbo.UnderstandingLevel where Id = @Id
end