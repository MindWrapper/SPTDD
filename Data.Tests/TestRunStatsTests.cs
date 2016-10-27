using System.Linq;
using NUnit.Framework;

namespace Data.Tests
{
	[TestFixture]
	public class TestRunStatsTests
	{
		private TestDataDbContext m_Context;
		private TestRunStats m_StatsService;

		[SetUp]
		public void Setup()
		{
			m_Context = new TestDataDbContext("TestConnection");
			m_StatsService = new TestRunStats(m_Context);
			CleanupDatabase();
		}

		[Test]
		public void GetRunStats_NoRunsInDatabase_ReturnsNothing()
		{
			var results = m_StatsService.GetRunStats();
			Assert.IsEmpty(results);
		}

		[Test]
		public void GetRunStats_OneTestRunInDatabase_ReturnsStatsEntry()
		{
			var tr = new TestRun() { Test = "TestName" };
			SaveTestRunToTheDatabase(tr);

			var results = m_StatsService.GetRunStats();

			Assert.That(results.Count(), Is.EqualTo(1));
		}

		[Test]
		public void GetRunStats_OneSuccesfullTestRunInTheDatabase_ReturnsStatsEntryWith100ProcentRate()
		{
			var tr = new TestRun { Test = "TestName", Success = true };
			SaveTestRunToTheDatabase(tr);

			var statEntry = m_StatsService.GetRunStats().First();

			Assert.That(statEntry.Rate, Is.EqualTo(100));
		}

		[Test]
		public void GetRunStats_OneFailedTestRunInTheDatabase_ReturnsStatsEntryWith0ProcentRate()
		{
			var tr = new TestRun { Test = "TestName", Success = false };
			SaveTestRunToTheDatabase(tr);

			var statEntry = m_StatsService.GetRunStats().First();

			Assert.That(statEntry.Rate, Is.EqualTo(0));
		}

		[Test]
		public void GetRunStats_SameTestFailedAndSucceeded_ReturnsStatsEntryWith0ProcentRate()
		{
			var trFailed = new TestRun { Test = "TestName", Success = false };
			var trSucceded = new TestRun {Test  = "TestName", Success = true };
			SaveTestRunToTheDatabase(trFailed);
			SaveTestRunToTheDatabase(trSucceded);

			var statEntry = m_StatsService.GetRunStats().First();

			Assert.That(statEntry.Rate, Is.EqualTo(50));
		}

		[Test]
		[Ignore("Exercise: Enable it an make it green. Don't foget to uncomment the assertion.")]
		public void GetRunStats_OneTestRunInDatabase_ReturnsStatsEntryWithProperName()
		{
			var tr = new TestRun() { Test = "TestName" };
			SaveTestRunToTheDatabase(tr);

			var statEntry = m_StatsService.GetRunStats().First();

			// DON'T forget to uncomment, when enabling the test
			// Assert.That(statEntry.Name, Is.EqualTo("TestName"));
		}

		private void SaveTestRunToTheDatabase(TestRun tr)
		{
			m_Context.TestRuns.Add(tr);
			m_Context.SaveChanges();
		}

		private void CleanupDatabase()
		{
			m_Context.Database.ExecuteSqlCommand("delete from TestRuns");
		}
	}
}
