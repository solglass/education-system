create proc [dbo].[CleanDatabase]
as
Begin
	-- disable all constraints
	exec sp_MSForEachTable "ALTER TABLE ? NOCHECK CONSTRAINT all"
	-- delete data in all tables
	exec sp_MSForEachTable "DELETE FROM ?"
	-- enable all constraints
	exec sp_MSForEachTable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"
	-- reset indentity columns
	exec sp_MSForEachTable "DBCC CHECKIDENT ( '?', RESEED, 0)"
	-- fill data in Enum tables 
	exec FillEnumTables
End
