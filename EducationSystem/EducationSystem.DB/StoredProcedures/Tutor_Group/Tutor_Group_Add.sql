Create Proc [dbo].[Tutor_Group_Add]
(
@UserID int,
@GroupID int)
as
begin
Insert Into dbo.Tutor_Group(UserID,GroupID) Values(@UserID,@GroupID)
select SCOPE_IDENTITY()
end
