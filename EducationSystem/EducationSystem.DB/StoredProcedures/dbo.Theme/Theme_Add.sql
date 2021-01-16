CREATE proc [dbo].[Theme_Add]
(@name nvarchar (100))
as
begin
insert into dbo.Theme (Name)
values(@name)
end
