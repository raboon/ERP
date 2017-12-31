DECLARE @dbName varchar(60);
Set @dbName = 'StadiumDev';
use master;
DECLARE @SQL varchar(max);

SET @SQL = 'alter database ' + @dbName +' set single_user with rollback immediate;
drop database ' + @dbName + ';'
exec(@SQL);
waitfor Delay '00:00:02';

SET @SQL = 'create database ' + @dbName + ';'
exec(@SQL);
waitfor Delay '00:00:02';

SET @SQL = 'use ' + @dbName + '; create user TFRapp;

EXEC sp_addrolemember ''db_owner'', ''TFRapp'';
EXEC sp_addrolemember ''db_datareader'', ''TFRapp'';
EXEC sp_addrolemember ''db_datawriter'', ''TFRapp'';'
exec(@SQL);
use master