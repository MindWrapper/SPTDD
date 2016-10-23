using NUnit.Framework;

namespace Data.Tests
{
	[TestFixture]
	public class TestRunStatsTests
	{
		[Test]
		public void GetRunStats_NoRunsInDatabase_ReturnsNothing()
		{
			var context = new TestDataDbContext("TestConnection");
			var statsService = new TestRunStats(context);
			var results = statsService.GetRunStats();
			Assert.IsEmpty(results);
		}
	}
}
