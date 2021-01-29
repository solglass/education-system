create proc [dbo].[GroupStatus_Add] (
	@name nvarchar(50)
 ) as
 begin
	insert into dbo.GroupStatus (Name)
	values (@name)
 end
GO


