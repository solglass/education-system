create proc FillEnumTables
as
Begin
	insert into AttachmentType(Id, Name) values
	(1, 'File'),
	(2, 'Link')
	insert into GroupStatus(Id, Name) values
	(1, 'Recruitment'),
	(2, 'ReadyToStart'),
	(3, 'InProgress'),
	(4, 'Finished')
	insert into HomeworkAttemptStatus(Id, Name) values
	(1, 'ToDo'),
	(2, 'Expired'),
	(3, 'ToCheck'),
	(4, 'ToFix'),
	(5, 'Passed'),
	(6, 'PassedLate')
	insert into [Role](Id, Name) values
	(1, 'Admin'),
	(2, 'Student'),
	(3, 'Teacher'),
	(4, 'Tutor'),
	(5, 'Methodist'),
	(6, 'Manager')
	insert into UnderstandingLevel(Id, Name) values
	(1, 'Bad'),
	(2, 'Medium'),
	(3, 'Good')
end