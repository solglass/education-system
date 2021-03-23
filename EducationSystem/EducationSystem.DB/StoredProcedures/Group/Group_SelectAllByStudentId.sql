create proc dbo.[Group_SelectAllByStudentId]
(@studentId int) as
begin
	select 
		sg.GroupId
	from Student_Group sg 
	where sg.UserId = @studentId
end