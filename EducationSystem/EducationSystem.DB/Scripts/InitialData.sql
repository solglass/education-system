declare @DbVersion int

select top 1 @DbVersion = version
from dbo.DbVersion order by id desc

if @DbVersion is not null set noexec on

INSERT INTO dbo.AttachmentType VALUES
	(1, 'File'),
	(2, 'Link')
GO

INSERT INTO dbo.GroupStatus VALUES
	(1, 'Recruitment'),
	(2, 'ReadyToStart'),
	(3, 'InProgress'),
	(4, 'Finished')
GO

INSERT INTO dbo.HomeworkAttemptStatus VALUES
	(1,'ToDo'),
	(2,'Expired'),
	(3,'ToCheck'),
	(4,'ToFix'),
	(5,'Passed'),
	(6,'PassedLate')
GO

INSERT INTO dbo.[Role]	VALUES
	(1,'Admin'),
	(2,'Student'),
	(3,'Teacher'),
	(4, 'Tutor'),
	(5, 'Methodist'),
	(6,'Manager')
GO

INSERT INTO dbo.UnderstandingLevel VALUES
	(1,'Bad'),
	(2,'Medium'),
	(3, 'Good')
GO
INSERT INTO dbo.DbVersion VALUES
	(1)
GO

set noexec off