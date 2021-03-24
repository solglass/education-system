create proc dbo.[Group_SelectAllByTeacherId]
(@teacherId int) as
begin
	select 
		sg.GroupId
	from Teacher_Group sg 
	where sg.UserId = @teacherId
end