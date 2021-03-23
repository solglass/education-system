create proc dbo.[Group_SelectAllByTutorId]
(@tutorId int) as
begin
	select 
		sg.GroupId
	from Tutor_Group sg 
	where sg.UserId = @tutorId
end