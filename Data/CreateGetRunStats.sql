CREATE PROCEDURE [dbo].[GetRunStats] 
AS 
BEGIN
select 100 * SUM (CASE WHEN Success = 1 THEN 1 ELSE 0 END) / COUNT (Test)
from TestRuns
group by Test
END