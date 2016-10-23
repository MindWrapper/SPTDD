using System.Linq;
using NUnit.Framework;

namespace Data.Tests
{
	[TestFixture]
	public class TestRunStatsTests
	{
		[SetUp]
		public void Setup()
		{
			CleanupDatabase();
		}

		[Test]
		public void GetRunStats_NoRunsInDatabase_ReturnsNothing()
		{
			var context = new TestDataDbContext("TestConnection");
			var statsService = new TestRunStats(context);
			var results = statsService.GetRunStats();
			Assert.IsEmpty(results);
		}

		[Test]
		public void GetRunStats_OneTestRunInDatabase_ReturnsStatsEntry()
		{
			var context = new TestDataDbContext("TestConnection");
			var statsService = new TestRunStats(context);
			var tr = new TestRun();
			context.TestRuns.Add(tr);
			context.SaveChanges();

			var results = statsService.GetRunStats();

			Assert.That(results.Count(), Is.EqualTo(1));
		}

		private static void CleanupDatabase()
		{
			var context = new TestDataDbContext("TestConnection");
			context.Database.ExecuteSqlCommand("delete from TestRuns");
		}
	}
}
