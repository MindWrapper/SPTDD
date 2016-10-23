CREATE PROCEDURE [dbo].[GetRunStats] 
AS 
BEGIN
select Success * 100 from TestRuns
END