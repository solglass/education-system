create proc [dbo].[Feedback_Delete](@Id int)
as
begin
Delete from dbo.Feedback where Id = @Id
end