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
			var tr = new TestRun();
			m_Context.TestRuns.Add(tr);
			m_Context.SaveChanges();

			var results = m_StatsService.GetRunStats();

			Assert.That(results.Count(), Is.EqualTo(1));
		}

		[Test]
		public void GetRunStats_OneSuccesfullTestRunInTheDatabase_ReturnsStatsEntryWith100ProcentRate()
		{
			var tr = new TestRun();
			tr.Success = true;
			m_Context.TestRuns.Add(tr);
			m_Context.SaveChanges();

			var statEntry = m_StatsService.GetRunStats().First();

			Assert.That(statEntry.Rate, Is.EqualTo(100));
		}

		private void CleanupDatabase()
		{
			m_Context.Database.ExecuteSqlCommand("delete from TestRuns");
		}
	}
}
