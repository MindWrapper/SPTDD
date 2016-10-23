using System.Collections.Generic;
using System.Data;

namespace Data
{
	public class TestRunStat
	{
		public int Rate;
	}
	public class TestRunStats
	{
		private readonly TestDataDbContext m_Context;

		public TestRunStats(TestDataDbContext context)
		{
			m_Context = context;
		}

		public IEnumerable<TestRunStat> GetRunStats()
		{
			var database = m_Context.Database;
			var cmd = database.Connection.CreateCommand();
			cmd.CommandText = "GetRunStats";
			cmd.CommandType = CommandType.StoredProcedure;
			var results = new List<TestRunStat>();
			try
			{
				database.Connection.Open();
				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					var statsEntry = new TestRunStat();
					statsEntry.Rate = reader.GetInt32(0);
					results.Add(statsEntry);
				}
			}
			finally
			{
				database.Connection.Close();
			}
			return results;
		}
	}
}
